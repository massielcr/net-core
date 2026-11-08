using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSCleanArchitectureGC.Application.Features.Categories.Commands.CreateCategory;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesList;
using PSCleanArchitectureGC.Application.Features.Categories.Queries.GetCategoriesListWithEvents;

namespace PSCleanArchitectureGC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllCategories")]
        [ProducesResponseType(typeof(List<CategoryListVm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
        {
            var dtos = await _mediator.Send(new GetCategoriesListQuery());

            return Ok(dtos);
        }

        [HttpGet("allwithevents", Name = "GetCategoriesWithEvents")]
        [ProducesResponseType(typeof(List<CategoryEventListVm>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includeHistory)
        {
            GetCategoriesListWithEventsQuery getCategoriesListWithEventsQuery = new() { IncludeHistory = includeHistory };
            var dtos = await _mediator.Send(getCategoriesListWithEventsQuery);

            return Ok(dtos);
        }


        [HttpPost(Name = "AddCategory")]
        [ProducesResponseType(typeof(CreateCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCategoryDto>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreateCategoryDto createCategoryDto = await _mediator.Send(createCategoryCommand);

            return Ok(createCategoryDto);
        }
    }
}
