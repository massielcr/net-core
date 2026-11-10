using PSCleanArchitectureGC.Api.IntegrationTests.Base;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesList;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using System.Text.Json;

namespace PSCleanArchitectureGC.Api.IntegrationTests.Controllers
{
    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public CategoryControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetAllCategories_ReturnsAllCategories()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/all");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CategoryListVm>>(responseString);

            Assert.IsType<List<CategoryListVm>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCategoriesWithEvents_IncludeHistoryTrue_ReturnsHistoryAndFutureEvents()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/allwithevents");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CategoryEventListVm>>(responseString);

            Assert.IsType<List<CategoryEventListVm>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCategoriesWithEvents_IncludeHistoryFalse_ReturnsOnlyFutureEvents()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/category/allwithevents");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<CategoryEventListVm>>(responseString);

            Assert.IsType<List<CategoryEventListVm>>(result);
            Assert.NotEmpty(result);
        }
    }
}
