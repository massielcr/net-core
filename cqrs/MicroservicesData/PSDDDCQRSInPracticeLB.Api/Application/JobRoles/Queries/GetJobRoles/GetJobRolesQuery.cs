using MediatR;

namespace PSDDDCQRSInPracticeLB.Api.Application.JobRoles.Queries.GetJobRoles
{
    public record GetJobRolesQuery : IRequest<List<JobRoleDto>>;
}
