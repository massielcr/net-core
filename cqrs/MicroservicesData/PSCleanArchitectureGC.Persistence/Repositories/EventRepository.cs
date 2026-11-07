using Microsoft.EntityFrameworkCore;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboTicketDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate)
        {
            return !await _dbContext.Events.AnyAsync(e => e.Name.Equals(name) && e.Date.Date.Equals(eventDate.Date));
        }
    }
}
