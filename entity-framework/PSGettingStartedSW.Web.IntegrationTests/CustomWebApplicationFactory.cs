using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PSGettingStartedSW.Web.DataContext;

namespace PSGettingStartedSW.Web.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the existing DbContext registration
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<FilmDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Inject the Testcontainer connection string
                services.AddDbContext<FilmDbContext>(options =>
                    options.UseSqlServer(_connectionString)
                );
            });
        }
    }
}
