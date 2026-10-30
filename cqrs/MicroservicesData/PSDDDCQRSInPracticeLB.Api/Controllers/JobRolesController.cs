using Microsoft.AspNetCore.Mvc;
using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Mapper;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRolesController : ControllerBase
    {
        private readonly IJobRoleRepository _jobRoleRepository;

        public JobRolesController(IJobRoleRepository jobRoleRepository)
        {
            _jobRoleRepository = jobRoleRepository;
        }

        [HttpGet]
        public async Task<List<JobRoleDto>> GetAll()
        {
            var jobRoles = await _jobRoleRepository.GetAll();

            return jobRoles.Select(JobRoleMapper.ToDto).ToList();
        }
    }
}
