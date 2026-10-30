using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<Store>> GetAll();
    }
}
