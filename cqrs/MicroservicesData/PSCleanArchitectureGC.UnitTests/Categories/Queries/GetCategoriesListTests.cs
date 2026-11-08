using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesList;
using PSCleanArchitectureGC.Application.Profiles;
using PSCleanArchitectureGC.UnitTests.Mocks;
using Shouldly;

namespace PSCleanArchitectureGC.UnitTests.Categories.Queries
{
    public class GetCategoriesListTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        

        public GetCategoriesListTests()
        {
            MapperConfiguration mapperConfiguration = new(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }, NullLoggerFactory.Instance);

            _mapper = mapperConfiguration.CreateMapper();
            _categoryRepository = CategoryRepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task GetCategoriesListQuery_ReturnListOfCategoryListVm()
        {
            GetCategoriesListQueryHandler sut = new(_categoryRepository.Object, _mapper);

            List<CategoryListVm> result = await sut.Handle(new GetCategoriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<CategoryListVm>>(); 
            result.Count.ShouldBe(4);
        }
    }
}
