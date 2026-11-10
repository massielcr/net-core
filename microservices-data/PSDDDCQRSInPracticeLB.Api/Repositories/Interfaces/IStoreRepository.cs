using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        Task<bool> ExistsById(int storeId);
        Task <List<Store>> GetAll();
    }
}
