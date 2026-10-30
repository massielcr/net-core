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
                        .Include(e => e.StoreAssignments.Where(sa => sa.To == null))
                        .ThenInclude(sa => sa.Store)
                        .Include(e => e.StoreAssignments.Where(sa => sa.To == null))
                        .ThenInclude(sa => sa.JobRole)
                        .Include(e => e.Salaries.Where(s => s.To == null))
                        .OrderBy(s => s.FirstName)
                        .ThenBy(s => s.LastName)
                        .ToListAsync();
        }

        public async Task<Employee?> GetById(int id)
        {
            return await _context.Employees
                        .Include(e => e.Address)
                        .Include(e => e.StoreAssignments.Where(sa => sa.To == null))
                        .ThenInclude(sa => sa.Store)
                        .Include(e => e.StoreAssignments.Where(sa => sa.To == null))
                        .ThenInclude(sa => sa.JobRole)
                        .Include(e => e.Salaries.Where(s => s.To == null))
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

        public Task Delete(Employee employee)
        {
            employee.IsDeleted = true;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            employee.EndDate = today;

            foreach(var salary in employee.Salaries.Where(s => s.To == null))
            {
                salary.To = today;
            }

            foreach(var storeAssignment in employee.StoreAssignments.Where(s => s.To == null))
            {
                storeAssignment.To = today;
            }

            _context.Employees.Update(employee);
            return _context.SaveChangesAsync();
        }        
    }
}
