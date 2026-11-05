using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces
{
    public interface IEmployeeStoreAssignmentHistoryRepository
    {
        Task Add(StoreAssignmentHistory storeAssignmentHistory);
    }
}
