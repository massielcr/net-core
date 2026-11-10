using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
    }
}
