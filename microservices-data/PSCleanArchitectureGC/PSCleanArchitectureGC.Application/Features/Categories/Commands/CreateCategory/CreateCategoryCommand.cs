using MediatR;

namespace PSCleanArchitectureGC.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreateCategoryDto>
    {
        public string Name { get; set; } = string.Empty;
    }
}
