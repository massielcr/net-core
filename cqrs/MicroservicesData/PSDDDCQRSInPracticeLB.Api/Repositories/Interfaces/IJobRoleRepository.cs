using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IJobRoleRepository
    {
        Task<List<JobRole>> GetAll();
    }
}
