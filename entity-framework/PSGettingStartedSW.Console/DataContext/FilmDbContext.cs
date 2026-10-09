using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PSGettingStartedSW.Console.Entities;
using PSGettingStartedSW.Console.Enums;

namespace PSGettingStartedSW.Console.DataContext
{
    public class FilmDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Film> Films => Set<Film>();
        public DbSet<Actor> Actors => Set<Actor>();

        public FilmDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? configurationString = _configuration.GetConnectionString("FilmDb");

            //optionsBuilder.LogTo(Console.WriteLine);

            optionsBuilder.UseSqlServer(configurationString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>()
                .HasData(
                    new
                    {
                        Id = 1,
                        Title = "The Shawshank Redemption",
                        Year = 1994,
                        Length = 182,
                        RatingScore = 9.3,
                        Mpaa = "R"
                    },
                    new
                    {
                        Id = 2,
                        Title = "The Dark Knight",
                        Year = 2008,
                        Length = 152,
                        RatingScore = 9.1,
                        Mpaa = "PG-13"
                    },
                    new
                    {
                        Id = 3,
                        Title = "The Godfather",
                        Year = 1972,
                        Length = 175,
                        RatingScore = 9.2,
                        Mpaa = "R"
                    },
                    new
                    {
                        Id = 4,
                        Title = "Pulp Fiction",
                        Year = 1994,
                        Length = 154,
                        RatingScore = 8.8,
                        Mpaa = "R"
                    }
                );

            modelBuilder.Entity<Actor>()
                .HasData(
                    new
                    {
                        Id = 1,
                        FirstName = "Tim",
                        LastName = "Robbins",
                        Age = 64,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000209/",
                        FilmId = 1
                        
                    },
                    new
                    {
                        Id =2,
                        FirstName = "Morgan",
                        LastName = "Freeman",
                        Age = 80,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000151/",
                        FilmId = 1
                    },
                    new
                    {
                        Id = 3,
                        FirstName = "Christian",
                        LastName = "Bale",
                        Age = 50,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000288/",
                        FilmId = 2
                    },
                    new
                    {
                        Id = 4,
                        FirstName = "Marlon",
                        LastName = "Brando",
                        Age = 93,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000008/",
                        FilmId = 3
                    },
                    new
                    {
                        Id = 5,
                        FirstName = "Al",
                        LastName = "Pacino",
                        Age = 83,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000199/",
                        FilmId = 3
                    },
                    new
                    {
                        Id = 6,
                        FirstName = "John",
                        LastName = "Travolta",
                        Age = 73,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000237/",
                        FilmId = 4
                    },
                    new
                    {
                        Id = 7,
                        FirstName = "Uma",
                        LastName = "Thurman",
                        Age = 71,
                        Gender = Gender.F,
                        ImbLink = "https://www.imdb.com/name/nm0000235/",
                        FilmId = 4
                    },
                    new
                    {
                        Id = 8,
                        FirstName = "Samuel L.",
                        LastName = "Jackson",
                        Age = 84,
                        Gender = Gender.M,
                        ImbLink = "https://www.imdb.com/name/nm0000168/",
                        FilmId = 4
                    }
                );
        }
    }
}
