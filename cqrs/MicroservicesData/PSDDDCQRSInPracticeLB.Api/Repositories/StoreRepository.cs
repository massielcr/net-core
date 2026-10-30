using Microsoft.EntityFrameworkCore;
using PSDDDCQRSInPracticeLB.Api.Data;
using PSDDDCQRSInPracticeLB.Api.Entities;
using PSDDDCQRSInPracticeLB.Api.Repositories.Interfaces;

namespace PSDDDCQRSInPracticeLB.Api.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreContext _context;

        public StoreRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<Store>> GetAll()
        {
            return await _context.Stores.OrderBy(s => s.Name).ToListAsync();
        }
    }
}
