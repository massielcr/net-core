using Microsoft.EntityFrameworkCore;
using PSCleanArchitectureGC.Domain.Entities;
using Shouldly;

namespace PSCleanArchitectureGC.Persistence.IntegrationTests
{
    public class GloboTicketDbContextTests
    {
        [Fact]
        public async Task Save_SetsCreatedByToSystem()
        {
            var databasePath = Path.Combine(Path.GetTempPath(), $"globoticket-persistence-{Guid.NewGuid():N}.db");
            var options = new DbContextOptionsBuilder<GloboTicketDbContext>()
                .UseSqlite($"Data Source={databasePath};Pooling=False;")
                .Options;

            try
            {
                await using var context = new GloboTicketDbContext(options);
                await context.Database.MigrateAsync();

                var _event = new Event
                {
                    EventId = Guid.NewGuid(),
                    Name = "Test event",
                    CategoryId = Guid.Parse("B0788D2F-8003-43C1-92A4-EDC76A7C5DDE")
                };
                context.Events.Add(_event);
                await context.SaveChangesAsync();

                _event.CreatedBy.ShouldBe("System");
            }
            finally
            {
                File.Delete(databasePath);
            }

        }
    }
}
