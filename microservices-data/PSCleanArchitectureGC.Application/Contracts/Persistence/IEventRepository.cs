using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Application.Contracts.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate);
    }
}
