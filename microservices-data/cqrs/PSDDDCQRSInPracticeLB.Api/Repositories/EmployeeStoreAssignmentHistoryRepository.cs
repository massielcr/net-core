using PSDDDCQRSInPracticeLB.Api.Data;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Repositories
{
    public class EmployeeStoreAssignmentHistoryRepository : IEmployeeStoreAssignmentHistoryRepository
    {
        private readonly StoreContext _context;

        public EmployeeStoreAssignmentHistoryRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(StoreAssignmentHistory storeAssignmentHistory)
        {
            _context.StoreAssignmentHistories.Add(storeAssignmentHistory);
        }
    }
}
