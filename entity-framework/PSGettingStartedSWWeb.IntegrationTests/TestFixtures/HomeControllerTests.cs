using NUnit.Framework;
using PSGettingStartedSW.Web.Entities;
using System.Net;

namespace PSGettingStartedSW.Web.IntegrationTests.TestFixtures
{
    [TestFixture]
    public class HomeControllerTests : IntegrationTestBase
    {
        [Test]
        public async Task Index_WhenCalled_ReturnsSuccessAndRendersHtmlView()
        {
            // Arrange - Seed sample movies inside the SQL container database
            var poorFilm = new Film { Title = "Pulp Fiction", Year = 1994, RatingScore = 8.8 };
            var greatFilm = new Film { Title = "The Godfather", Year = 1972, RatingScore = 9.2 };
            DbContext.Films.AddRange(greatFilm, poorFilm);
            await DbContext.SaveChangesAsync();

            // Act - Fetch the Index action
            var response = await Client.GetAsync("/");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Verify that the server returned an HTML webpage text document
            Assert.That(response.Content.Headers.ContentType!.MediaType, Is.EqualTo("text/html"));

            // Verify movies are present on the view
            var htmlContent = await response.Content.ReadAsStringAsync();
            Assert.That(htmlContent, Does.Contain("Pulp Fiction"));
            Assert.That(htmlContent, Does.Contain("The Godfather"));
        }
    }
}