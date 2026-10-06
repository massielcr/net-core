using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSGettingStartedSW.Entity;

namespace PSGettingStartedSW.DataContext
{
    public class FilmDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Film> Films => Set<Film>();

        public FilmDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? configurationString = _configuration.GetConnectionString("FilmDb");

            optionsBuilder.UseSqlServer(configurationString);
        }
    }
}
