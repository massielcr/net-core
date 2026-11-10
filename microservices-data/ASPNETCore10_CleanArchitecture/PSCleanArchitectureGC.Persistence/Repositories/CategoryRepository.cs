using Microsoft.EntityFrameworkCore;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(GloboTicketDbContext dbContext) : base(dbContext) { }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
        {
            var allCategories = await _dbContext.Categories.Include(c => c.Events).ToListAsync();

            if (!includePassedEvents)
            {
                allCategories.ForEach(c => c.Events = c.Events?.Where(e => e.Date >= DateTime.Today).ToList());
            }

            return allCategories;
        }
    }
}
