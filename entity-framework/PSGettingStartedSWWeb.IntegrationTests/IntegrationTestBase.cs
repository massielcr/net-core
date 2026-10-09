using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PSGettingStartedSW.Web.DataContext;
using Testcontainers.MsSql;

namespace PSGettingStartedSW.Web.IntegrationTests
{
    [TestFixture]
    public abstract class IntegrationTestBase
    {
        private MsSqlContainer _msSqlContainer = null!;
        private IServiceScope _scope = null!;

        protected WebApplicationFactory<Program> Factory { get; private set; } = null!;
        protected HttpClient Client { get; private set; } = null!;
        protected FilmDbContext DbContext { get; private set; } = null!;
        

        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            // 1. Initialize and start the SQL Server Testcontainer
            _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
                              .Build();

            await _msSqlContainer.StartAsync();

            // 2. Build WebApplicationFactory, overriding EF Core to use the container
            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FilmDbContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<FilmDbContext>(options =>
                            options.UseSqlServer(_msSqlContainer.GetConnectionString())
                        );
                    });
                });

            // Create HTTP Client to hit the API endpoints
            Client = Factory.CreateClient();

            // Create a dedicated scope to allow tests to arrange seed data or assert directly
            _scope = Factory.Services.CreateScope();
            DbContext = _scope.ServiceProvider.GetRequiredService<FilmDbContext>();

            // Run database migrations once to set up tables
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
            await Factory.DisposeAsync();
            await _msSqlContainer.StopAsync();
        }
    }
}
