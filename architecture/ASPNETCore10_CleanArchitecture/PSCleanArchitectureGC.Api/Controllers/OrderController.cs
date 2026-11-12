using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSCleanArchitectureGC.Application.Features.Orders.Queries.GetOrdersForMonth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PSCleanArchitectureGC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("for-month", Name = "GetOrdersForMonth")]
        [ProducesResponseType(typeof(List<GetOrdersForMonthQuery>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<OrdersForMonthDto>>> GetOrdersForMonth(DateTime date)
        {
            GetOrdersForMonthQuery getOrdersForMonthQuery = new() { Date = date };
            var dtos = await _mediator.Send(getOrdersForMonthQuery);

            return Ok(dtos);
        }
    }
}
