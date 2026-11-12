using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSCleanArchitectureGC.Application.Features.Events.Commands.CreateEvent;
using PSCleanArchitectureGC.Application.Features.Events.Commands.DeleteEvent;
using PSCleanArchitectureGC.Application.Features.Events.Commands.UpdateEvent;
using PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventDetail;
using PSCleanArchitectureGC.Application.Features.Events.Queries.GetEventsList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSCleanArchitectureGC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(typeof(List<EventListVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EventListVM>>> GetAllEvents()
        {
            var dtos = await _mediator.Send(new GetEventsListQuery());

            return Ok(dtos);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        [ProducesResponseType(typeof(EventDetailVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventDetailVM>> GetEventById(Guid id)
        {
            GetEventDetailQuery getEventDetailQuery = new() { Id = id };
            var dto = await _mediator.Send(getEventDetailQuery);

            return Ok(dto);
        }

        [HttpPost(Name = "AddEvent")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand)
        {
            Guid id = await _mediator.Send(createEventCommand);

            return Ok(id);
        }

        [HttpPut(Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
        {
            await _mediator.Send(updateEventCommand);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(Guid id)
        {
            DeleteEventCommand deleteEventCommand = new(){ EventId = id };
            await _mediator.Send(deleteEventCommand);

            return NoContent();
        }
    }
}
