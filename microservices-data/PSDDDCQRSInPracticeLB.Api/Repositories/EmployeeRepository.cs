using Microsoft.EntityFrameworkCore;
using PSDDDCQRSInPracticeLB.Api.Data;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StoreContext _context;

        public EmployeeRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employees
                        .Include(e => e.Address)
                        .Include(e => e.StoreAssignments)
                        .ThenInclude(sa => sa.Store)
                        .Include(e => e.StoreAssignments)
                        .ThenInclude(sa => sa.JobRole)
                        .Include(e => e.Salary)
                        .OrderBy(s => s.FirstName)
                        .ThenBy(s => s.LastName)
                        .ToListAsync();
        }

        public async Task<Employee?> GetById(int id)
        {
            return await _context.Employees
                        .Include(e => e.Address)
                        .Include(e => e.StoreAssignments)
                        .ThenInclude(sa => sa.Store)
                        .Include(e => e.StoreAssignments)
                        .ThenInclude(sa => sa.JobRole)
                        .Include(e => e.Salary)
                        .OrderBy(s => s.FirstName)
                        .ThenBy(s => s.LastName)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<Employee> Create(Employee employee)
        {
            _context.Employees.Add(employee);

            return _context.SaveChangesAsync().ContinueWith(t => employee);
        }

        public async Task Update(Employee employee)
        {
            await _context.SaveChangesAsync();
        }        
    }
}
