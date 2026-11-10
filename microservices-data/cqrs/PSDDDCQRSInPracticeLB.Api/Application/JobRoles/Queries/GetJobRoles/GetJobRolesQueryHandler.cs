using MediatR;
using PSDDDCQRSInPracticeLB.Api.Mapper;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.JobRoles.Queries.GetJobRoles
{
    public sealed class GetJobRolesQueryHandler : IRequestHandler<GetJobRolesQuery, List<JobRoleDto>>
    {
        private readonly IJobRoleRepository _jobRoleRepository;

        public GetJobRolesQueryHandler(IJobRoleRepository jobRoleRepository)
        {
            _jobRoleRepository = jobRoleRepository;
        }

        public async Task<List<JobRoleDto>> Handle(GetJobRolesQuery request, CancellationToken cancellationToken)
        {
            var jobRoles = await _jobRoleRepository.GetAll();
            return jobRoles.Select(JobRoleMapper.ToDto).ToList();
        }
    }
}
