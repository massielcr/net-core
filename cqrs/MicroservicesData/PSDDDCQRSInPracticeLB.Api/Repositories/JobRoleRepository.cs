using Microsoft.EntityFrameworkCore;
using PSDDDCQRSInPracticeLB.Api.Data;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Repositories
{
    public class JobRoleRepository : IJobRoleRepository
    {
        private readonly StoreContext _context;

        public JobRoleRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistById(int jobRoleId)
        {
            return await _context.JobRoles.AnyAsync(s => s.Id == jobRoleId);
        }

        public async Task<List<JobRole>> GetAll()
        {
            return await _context.JobRoles.OrderBy(s => s.Name).ToListAsync();
        }
    }
}
