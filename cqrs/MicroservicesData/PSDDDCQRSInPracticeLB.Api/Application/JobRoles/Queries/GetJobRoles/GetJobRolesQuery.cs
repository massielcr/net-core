using PSDDDCQRSInPracticeLB.Api.Application.Common.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Application.JobRoles.Queries.GetJobRoles
{
    public record GetJobRolesQuery : IQuery<List<JobRoleDto>>;
}
