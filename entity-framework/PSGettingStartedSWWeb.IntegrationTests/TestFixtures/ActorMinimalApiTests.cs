using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PSGettingStartedSW.Web.Entities;
using PSGettingStartedSW.Web.Enums;
using PSGettingStartedSW.Web.IntegrationTests;
using System.Net;
using System.Net.Http.Json;

namespace PSGettingStartedSW.Web.IntegrationTests.TestFixtures
{
    [TestFixture]
    public class ActorMinimalApiTests : IntegrationTestBase
    {
        [Test]
        public async Task GetActorsForFilm_WhenFilmDoesNotExist_ReturnsNotFound()
        {
            // Act - Requesting actors for a non-existent Film ID (99)
            var response = await Client.GetAsync("/api/films/99/actors");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetActorsForFilm_WhenFilmExists_ReturnsOkWithActors()
        {
            // Arrange
            var film = new Film
            {
                Title = "The Shawshank Redemption",
                Year = 1994,
                Actors = new List<Actor>
                {
                    new Actor { FirstName = "Tim", LastName = "Robbins" },
                    new Actor { FirstName = "Morgan", LastName = "Freeman" }
                }
            };
            DbContext.Films.Add(film);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetAsync($"/api/films/{film.Id}/actors");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var actors = await response.Content.ReadFromJsonAsync<List<Actor>>();
            Assert.That(actors, Is.Not.Null);
            Assert.That(actors!.Count, Is.EqualTo(2));
            Assert.That(actors.Any(a => a.FirstName == "Tim"), Is.True);
        }

        [Test]
        public async Task GetActorById_WhenActorDoesNotExistOnFilm_ReturnsNotFound()
        {
            // Arrange
            var film = new Film { Title = "Interstellar", Year = 2014 };
            DbContext.Films.Add(film);
            await DbContext.SaveChangesAsync();

            // Act - Trying to get actor ID 99 from a valid film
            var response = await Client.GetAsync($"/api/films/{film.Id}/actors/99");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetActorById_WhenActorExistsOnFilm_ReturnsOkWithActor()
        {
            // Arrange
            var actor = new Actor { FirstName = "Matthew", LastName = "McConaughey" };
            var film = new Film
            {
                Title = "Interstellar",
                Year = 2014,
                Actors = new List<Actor> { actor }
            };
            DbContext.Films.Add(film);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.GetAsync($"/api/films/{film.Id}/actors/{actor.Id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var result = await response.Content.ReadFromJsonAsync<Actor>();
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.FirstName, Is.EqualTo("Matthew"));
            Assert.That(result.LastName, Is.EqualTo("McConaughey"));
        }

        [Test]
        public async Task CreateActor_WhenFilmDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var newActor = new Actor { FirstName = "Tom", LastName = "Hanks" };

            // Act - Sending actor payload to an invalid film route (ID 99)
            var response = await Client.PostAsJsonAsync("/api/films/99/actors", newActor);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task CreateActor_WhenFilmExists_PersistsActorAndReturnsCreatedAtRoute()
        {
            // Arrange
            var film = new Film { Title = "Forrest Gump", Year = 1994 };
            DbContext.Films.Add(film);
            await DbContext.SaveChangesAsync();

            var payload = new Actor
            {
                FirstName = "Tom",
                LastName = "Hanks",
                Age = 67,
                Gender = Gender.Male,
                ImbLink = "https://imdb.com"
            };

            // Act
            var response = await Client.PostAsJsonAsync($"/api/films/{film.Id}/actors", payload);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(response.Headers.Location, Is.Not.Null);

            // Assert routing location maps out correctly to Route Name: "GetActor"
            Assert.That(response.Headers.Location!.ToString(), Does.Contain($"/api/films/{film.Id}/actors/"));

            // Verify structural database save direct via DbContext
            DbContext.ChangeTracker.Clear();
            Actor? savedActor = await DbContext.Actors.Where(a => a.FirstName == "Tom" && a.LastName == "Hanks").FirstOrDefaultAsync();

            Assert.That(savedActor, Is.Not.Null);
            Assert.That(savedActor!.Age, Is.EqualTo(67));
        }

        [Test]
        public async Task UpdateActor_WhenActorDoesNotExist_ReturnsBadRequest()
        {
            // Arrange
            var updatePayload = new Actor { FirstName = "John", LastName = "Doe" };

            // Act - Attempting put on target ID 99
            var response = await Client.PutAsJsonAsync("/api/actors/99", updatePayload);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task UpdateActor_WhenActorExists_UpdatesFieldsAndReturnsOk()
        {
            // Arrange
            var actor = new Actor { FirstName = "Christian", LastName = "Bale", Age = 50, Gender = Gender.Male };
            DbContext.Actors.Add(actor);
            await DbContext.SaveChangesAsync();

            var updatePayload = new Actor
            {
                FirstName = "Chris",
                LastName = "Bale",
                Age = 51,
                Gender = Gender.Male,
                ImbLink = "https://imdb.com"
            };

            // Act
            var response = await Client.PutAsJsonAsync($"/api/actors/{actor.Id}", updatePayload);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Verify direct mutations inside SQL testcontainer
            DbContext.ChangeTracker.Clear();
            var updatedActor = await DbContext.Actors.FindAsync(actor.Id);

            Assert.That(updatedActor, Is.Not.Null);
            Assert.That(updatedActor!.FirstName, Is.EqualTo("Chris"));
            Assert.That(updatedActor.Age, Is.EqualTo(51));
            Assert.That(updatedActor.ImbLink, Is.EqualTo("https://imdb.com"));
        }

        [Test]
        public async Task DeleteActor_WhenActorDoesNotExist_ReturnsNotFound()
        {
            // Act
            var response = await Client.DeleteAsync("/api/actors/99");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task DeleteActor_WhenActorExists_RemovesFromDatabaseAndReturnsOk()
        {
            // Arrange
            var actor = new Actor { FirstName = "Cillian", LastName = "Murphy" };
            DbContext.Actors.Add(actor);
            await DbContext.SaveChangesAsync();

            // Act
            var response = await Client.DeleteAsync($"/api/actors/{actor.Id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Confirm physical database record extraction yields nothing
            DbContext.ChangeTracker.Clear();
            var actorExists = await DbContext.Actors.AnyAsync(a => a.Id == actor.Id);

            Assert.That(actorExists, Is.False);
        }
    }
}
