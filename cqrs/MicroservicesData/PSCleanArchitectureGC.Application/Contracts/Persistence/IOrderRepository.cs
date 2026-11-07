using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<List<Order>> GetOrdersForMonth(DateTime date);
    }
}
