using Microsoft.EntityFrameworkCore;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(GloboTicketDbContext dbContext) : base(dbContext) { }

        public async Task<List<Order>> GetOrdersForMonth(DateTime date)
        {
            return await _dbContext.Orders.Where(o => o.OrderPlaced.Month == date.Month && o.OrderPlaced.Year == date.Year)
                                                  .AsNoTracking()
                                                  .ToListAsync();
        }
    }
}
