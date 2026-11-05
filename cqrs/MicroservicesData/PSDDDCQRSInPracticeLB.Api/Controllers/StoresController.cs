using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSDDDCQRSInPracticeLB.Api.Application.Stores.Queries.GetStores;

namespace PSDDDCQRSInPracticeLB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<StoreDto>> GetAll()
        {
            return await _mediator.Send(new GetStoresQuery());
        }
    }
}
