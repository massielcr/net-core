using PSDDDCQRSInPracticeLB.Api.Data;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Repositories
{
    public class EmployeeSalaryHistoryRepository : IEmployeeSalaryHistoryRepository
    {
        private readonly StoreContext _context;

        public EmployeeSalaryHistoryRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task Add(EmployeeSalaryHistory salaryHistory)
        {
            _context.EmployeeSalaryHistories.Add(salaryHistory);
            await _context.SaveChangesAsync();
        }
    }
}
