using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PSGettingStartedSW.Web.Entities;
using PSGettingStartedSW.Web.IntegrationTests;
using System.Net;
using System.Net.Http.Json;

namespace PSGettingStartedSW.Web.IntegrationTests.TestFixtures
{
    [TestFixture]
    internal class FilmApiControllerTests : BaseIntegrationTest
    {
        [Test]
        public async Task GetFilms_WhenNoFilmsExist_ReturnsNotFound()
        {
            // Act
            var response = await Client.GetAsync("/api/films");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetFilms_WhenFilmsExist_ReturnsOkWithFilmsOrderedByYear()
        {
            // Arrange - Direct database seeding
            var olderFilm = new Film { Title = "Inception", Year = 2010, RatingScore = 8.8 };
            var newerFilm = new Film { Title = "Oppenheimer", Year = 2023, RatingScore = 8.9 };
            DbContext.Films.AddRange(newerFilm, olderFilm);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetAsync("/api/films");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var films = await response.Content.ReadFromJsonAsync<List<Film>>();
            Assert.That(films, Is.Not.Null);
            Assert.That(films!.Count, Is.EqualTo(2));
            Assert.That(films[0].Title, Is.EqualTo("Inception")); // Verify OrderBy(f => f.Year) works
        }

        [Test]
        public async Task GetFilm_WithValidId_ReturnsFilmWithActors()
        {
            // Arrange
            var film = new Film
            {
                Title = "The Matrix",
                Year = 1999,
                Actors = new List<Actor> { new Actor { FirstName = "Keanu", LastName = "Reeves" } }
            };
            DbContext.Films.Add(film);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetAsync($"/api/films/{film.Id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var result = await response.Content.ReadFromJsonAsync<Film>();
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("The Matrix"));
            Assert.That(result.Actors.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateFilm_WithValidPayload_PersistsDataAndReturnsCreatedAtRoute()
        {
            // Arrange
            var newFilm = new Film
            {
                Title = "Interstellar",
                Year = 2014,
                Length = 169,
                RatingScore = 8.6,
                Actors = new List<Actor> { new Actor { FirstName = "Matthew", LastName = "McConaughey" } }
            };

            // Act
            var response = await Client.PostAsJsonAsync("/api/films", newFilm);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);

            // Verify physical persistence directly inside the container database
            DbContext.ChangeTracker.Clear();
            var savedFilm = await DbContext.Films.Include(f => f.Actors).FirstOrDefaultAsync(f => f.Title == "Interstellar");

            Assert.That(savedFilm, Is.Not.Null);
            Assert.That(savedFilm!.Actors.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateFilm_WithExistingId_UpdatesFieldsAndReturnsOk()
        {
            // Arrange
            var originalFilm = new Film { Title = "Gladiator", Year = 2000, Length = 155, RatingScore = 8.5 };
            DbContext.Films.Add(originalFilm);
            await DbContext.SaveChangesAsync();

            var updatePayload = new Film { Title = "Gladiator Extended", Year = 2000, Length = 171, RatingScore = 8.6 };

            // Act
            var response = await Client.PutAsJsonAsync($"/api/films/{originalFilm.Id}", updatePayload);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            DbContext.ChangeTracker.Clear();
            var updatedFilm = await DbContext.Films.FindAsync(originalFilm.Id);
            Assert.That(updatedFilm!.Title, Is.EqualTo("Gladiator Extended"));
            Assert.That(updatedFilm.Length, Is.EqualTo(171));
        }

        [Test]
        public async Task DeleteFilm_WithValidId_RemovesFilmAndAssociatedActors()
        {
            // Arrange
            var targetFilm = new Film
            {
                Title = "Fake Movie",
                Year = 2026,
                Actors = new List<Actor> { new Actor { FirstName = "John", LastName = "Doe" } }
            };
            DbContext.Films.Add(targetFilm);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.DeleteAsync($"/api/films/{targetFilm.Id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Confirm cascading removals from the database tracking collections
            DbContext.ChangeTracker.Clear();
            var filmExists = await DbContext.Films.AnyAsync(f => f.Id == targetFilm.Id);
            var actorsExist = await DbContext.Actors.AnyAsync(a => a.FirstName == "John" && a.LastName == "Doe");

            Assert.That(filmExists, Is.False);
            Assert.That(actorsExist, Is.False);
        }
    }
}
