using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using PSCleanArchitectureGC.Application.Profiles;
using PSCleanArchitectureGC.UnitTests.Mocks;
using Xunit;

namespace PSCleanArchitectureGC.UnitTests.Categories.Queries
{
    public class GetCategoriesListWithEventsTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _categoryRepository;

        public GetCategoriesListWithEventsTests()
        {
            MapperConfiguration mapperConfiguration = new(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, NullLoggerFactory.Instance);

            _mapper = mapperConfiguration.CreateMapper();
            _categoryRepository = CategoryRepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task GetCategoriesListWithEventsQuery_IncludeHistoryTrue_ReturnsHistoryOfListOfCategoryEventListVm()
        {
            GetCategoriesListWithEventsQueryHandler sut = new(_categoryRepository.Object, _mapper);

            List<CategoryEventListVm> result = await sut.Handle(new GetCategoriesListWithEventsQuery() { IncludeHistory = true }, CancellationToken.None);

            // Verify the result is not null
            Assert.NotNull(result);

            // Verify the correct number of categories is returned
            Assert.Equal(4, result.Count);


            var categoryWithEvents = result.FirstOrDefault(c => c.Name == "Concerts");
            Assert.NotNull(categoryWithEvents);
            Assert.NotEmpty(categoryWithEvents.Events);
            Assert.Single(categoryWithEvents.Events);
            var concertEvent = categoryWithEvents.Events.First();
            Assert.Equal("Pink", concertEvent.Name);

            var musicalCategory = result.FirstOrDefault(c => c.Name == "Musicals");
            Assert.NotNull(musicalCategory);
            Assert.NotEmpty(musicalCategory.Events);
            Assert.Single(musicalCategory.Events);
            var musicalEvent = musicalCategory.Events.First();
            Assert.Equal("Waitress", musicalEvent.Name);

            var conferenceCategory = result.FirstOrDefault(c => c.Name == "Conferences");
            Assert.NotNull(conferenceCategory);
            Assert.True(conferenceCategory.Events == null || conferenceCategory.Events.Count == 0);

            var playCategory = result.FirstOrDefault(c => c.Name == "Plays");
            Assert.NotNull(playCategory);
            Assert.True(playCategory.Events == null || playCategory.Events.Count == 0);

            // Verify the mock was called with the correct parameter
            _categoryRepository.Verify(r => r.GetCategoriesWithEvents(true), Times.Once);
        }

        [Fact]
        public async Task GetCategoriesListWithEventsQuery_IncludeHistoryFalse_ReturnsOnlyFutureEvents()
        {
            GetCategoriesListWithEventsQueryHandler sut = new(_categoryRepository.Object, _mapper);

            List<CategoryEventListVm> result = await sut.Handle(new GetCategoriesListWithEventsQuery() { IncludeHistory = false }, CancellationToken.None);

            // Verify the result is not null
            Assert.NotNull(result);

            // Verify the correct number of categories is returned
            Assert.Equal(4, result.Count);

            var concertCategory = result.FirstOrDefault(c => c.Name == "Concerts");
            Assert.NotNull(concertCategory);
            Assert.NotEmpty(concertCategory.Events);
            Assert.Single(concertCategory.Events);
            var concertEvent = concertCategory.Events.First();
            Assert.Equal("Pink", concertEvent.Name);

            var conferenceCategory = result.FirstOrDefault(c => c.Name == "Conferences");
            Assert.NotNull(conferenceCategory);
            Assert.True(conferenceCategory.Events == null || conferenceCategory.Events.Count == 0);

            var playCategory = result.FirstOrDefault(c => c.Name == "Plays");
            Assert.NotNull(playCategory);
            Assert.True(playCategory.Events == null || playCategory.Events.Count == 0);

            // Verify the mock was called with the correct parameter
            _categoryRepository.Verify(r => r.GetCategoriesWithEvents(false), Times.Once);
        }
    }
}
