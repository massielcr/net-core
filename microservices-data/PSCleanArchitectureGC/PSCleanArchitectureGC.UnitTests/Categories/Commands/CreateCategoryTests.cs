using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Application.Features.Categories.Commands.CreateCategory;
using PSCleanArchitectureGC.Application.Profiles;
using PSCleanArchitectureGC.UnitTests.Mocks;
using Shouldly;

namespace PSCleanArchitectureGC.UnitTests.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CreateCategoryTests()
        {
            MapperConfiguration mapperConfiguration = new(cfg =>
            {
               cfg.AddProfile<MappingProfile>();
            }, NullLoggerFactory.Instance);


            _mapper = mapperConfiguration.CreateMapper();
            _mockCategoryRepository = CategoryRepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task CreateCategoryCommand_ValidCategory_ReturnCreateCategoryDto()
        {
            CreateCategoryCommandHandler sut = new(_mockCategoryRepository.Object, _mapper);

            await sut.Handle(new CreateCategoryCommand() { Name = "Test" }, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.ListAllAsync();

            allCategories.Count.ShouldBe(5);
        }
    }
}
