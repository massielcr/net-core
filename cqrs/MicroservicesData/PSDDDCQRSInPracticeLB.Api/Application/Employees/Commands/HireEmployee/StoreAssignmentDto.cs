using PSDDDCQRSInPracticeLB.Api.Application.JobRoles.Queries.GetJobRoles;
using PSDDDCQRSInPracticeLB.Api.Application.Stores.Queries.GetStores;

namespace PSDDDCQRSInPracticeLB.Api.Application.Employees.Commands.HireEmployee
{
    public class StoreAssignmentDto
    {
        public int StoreId { get; set; }
        public StoreDto? Store { get; set; }

        public int JobRoleId { get; set; }
        public JobRoleDto? JobRole { get; set; }
    }
}
