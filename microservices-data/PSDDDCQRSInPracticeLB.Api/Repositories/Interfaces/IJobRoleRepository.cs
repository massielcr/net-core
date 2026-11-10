using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IJobRoleRepository
    {
        Task<bool> ExistById(int jobRoleId);
        Task<List<JobRole>> GetAll();
    }
}
