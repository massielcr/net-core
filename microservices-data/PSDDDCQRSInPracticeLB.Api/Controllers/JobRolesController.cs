using MediatR;
using Microsoft.AspNetCore.Mvc;
using PSDDDCQRSInPracticeLB.Api.Application.JobRoles.Queries.GetJobRoles;

namespace PSDDDCQRSInPracticeLB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<JobRoleDto>> GetAll()
        {
            return await _mediator.Send(new GetJobRolesQuery());
        }
    }
}
