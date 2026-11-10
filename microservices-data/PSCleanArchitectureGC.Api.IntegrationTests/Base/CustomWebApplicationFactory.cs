using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PSCleanArchitectureGC.Persistence;

namespace PSCleanArchitectureGC.Api.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
    {
        private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"globoticket-tests-{Guid.NewGuid():N}.db");

        public HttpClient GetAnonymousClient() => CreateClient();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<GloboTicketDbContext>>();
                services.AddDbContext<GloboTicketDbContext>(options => options.UseSqlite($"Data Source={_databasePath}"));

                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<GloboTicketDbContext>();
                context.Database.Migrate();
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }
            throw new NotImplementedException();
        }
    }
}
