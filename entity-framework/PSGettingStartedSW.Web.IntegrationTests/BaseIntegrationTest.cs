using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PSGettingStartedSW.Web.DataContext;
using Testcontainers.MsSql;

namespace PSGettingStartedSW.Web.IntegrationTests
{
    [TestFixture]
    public abstract class BaseIntegrationTest
    {        
        protected HttpClient Client { get; private set; } = null!;
        protected FilmDbContext DbContext { get; private set; } = null!;


        private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").Build();
        protected CustomWebApplicationFactory<Program> _factory { get; private set; } = null!;
        private IServiceScope _scope = null!;        


        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            // 1. Asynchronously spin up the container safely
            await _msSqlContainer.StartAsync();

            // 2. Instantiate your factory by passing the live connection string
            _factory = new CustomWebApplicationFactory<Program>(_msSqlContainer.GetConnectionString());

            Client = _factory.CreateClient();

            // 3. Set up the local test scope and run migrations
            _scope = _factory.Services.CreateScope();
            DbContext = _scope.ServiceProvider.GetRequiredService<FilmDbContext>();

            await DbContext.Database.MigrateAsync();
        }

        [SetUp]
        public async Task SetUpAsync()
        {
            // Clear tracking memory so old tests don't pollute subsequent operations
            DbContext.ChangeTracker.Clear();

            // Wipe out data rows between tests. Child tables must be cleared first.
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Actors]");
            await DbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Films]");

            // Reseed sequences to 0 so the first item added in any test gets an ID of 1
            await DbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Actors', RESEED, 0)");
            await DbContext.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Films', RESEED, 0)");
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDownAsync()
        {
            _scope?.Dispose();
            Client?.Dispose();
            await _factory.DisposeAsync();
            await _msSqlContainer.StopAsync();
            await _msSqlContainer.DisposeAsync();
        }
    }
}
