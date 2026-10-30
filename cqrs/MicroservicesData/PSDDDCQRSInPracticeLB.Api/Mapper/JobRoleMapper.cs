using PSDDDCQRSInPracticeLB.Api.Dtos;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Mapper
{
    public static class JobRoleMapper
    {
        public static JobRoleDto ToDto(JobRole jobRole)
        {
            return new JobRoleDto()
            {
                Id = jobRole.Id,
                Name = jobRole.Name
            };
        }
    }
}
