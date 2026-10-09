using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSGettingStartedSW.Console.DataContext;
using PSGettingStartedSW.Console.Entities;
using PSGettingStartedSW.Console.Enums;
using System.Reflection;

Console.WriteLine("Starting App..");


var builder = Host.CreateApplicationBuilder();

builder.Services.AddDbContext<FilmDbContext>();

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());



var app = builder.Build();

var context = app.Services.GetRequiredService<FilmDbContext>();

context.Database.EnsureCreated();

Console.WriteLine("1. Select");

var query = context.Films
                 .OrderByDescending(f => f.Year)
                 .Select(f => f.Title);

Console.WriteLine($"Titles: {string.Join(", ", await query.ToArrayAsync())}");



Console.WriteLine("2. Paging");

var queryPaging = context.Films
                        .OrderByDescending(f => f.Year)
                        .Skip(1)
                        .Take(2)                        
                        .Select(f => f.Title);

Console.WriteLine($"Skip 1 Take 2 - Titles: {string.Join(", ", await queryPaging.ToArrayAsync())}");



Console.WriteLine("3. Opject Graph");

var queryOpjectGraph = context.Films
                              .Where(f => f.Actors.Any(a => a.FirstName == "Al"))
                              .Select(f => f.Title);

Console.WriteLine($"Actor's FirstName Al: {string.Join(", ", await queryOpjectGraph.FirstOrDefaultAsync())}");

var queryOpjectGraph2 = context.Films
                               .Include(f => f.Actors)
                               .Select(f => new { Title = f.Title, Actors = f.Actors.Select(a => $"{a.FirstName} {a.LastName}").ToList()});

var queryOpjectGraph2Result = await queryOpjectGraph2.FirstOrDefaultAsync();

Console.WriteLine($"Include Actors: {queryOpjectGraph2Result!.Title}:");
foreach(string a in queryOpjectGraph2Result.Actors)
{
    Console.WriteLine($"- {a}");
}



Console.WriteLine("4. Delete");

var filmToDelete = await context.Films.Include(f => f.Actors).Where(f => f.Title == "The Godfather Part II").FirstOrDefaultAsync();

if (filmToDelete is not null)
{
    foreach (Actor actor in filmToDelete.Actors)
    {
        context.Actors.Remove(actor);
    }
    context.Films.Remove(filmToDelete);

    Console.WriteLine($"The Godfather Part II was removed from the db: {(await context.SaveChangesAsync() > 0 ? "True" : "False")}");
}


Console.WriteLine("5. Create");

var godfather2 = new Film
{
    Title = "The Godfather Part II",
    Year = 1974,
    Length = 202,
    RatingScore = 9.0,
    Mpaa = "R",
    Actors = [
        new Actor
        {
            FirstName = "Al",
            LastName = "Pacino",
            Age = 83,
            Gender = Gender.M,
            ImbLink = "https://www.imdb.com/name/nm0000199/"
        },
        new Actor
        {
            FirstName = "Robert",
            LastName = "De Niro",
            Age = 84,
            Gender = Gender.M,
            ImbLink = "https://www.imdb.com/name/nm0000134/"
        }
    ]
};

if (!context.Films.Any(f => f.Title == "The Godfather Part II"))
{
    foreach (Actor actor in godfather2.Actors)
    {
        context.Actors.Add(actor);
    }
    context.Films.Add(godfather2);

    Console.WriteLine($"The Godfather Part II was added to the db: {(await context.SaveChangesAsync() > 0 ? "True" : "False")}");
}
else
{
    Console.WriteLine("The Godfather Part II was already in the db");
}



Console.WriteLine("6. Update");

Film? godfather2update = await context.Films.Where(f => f.Title == "The Godfather Part II").FirstOrDefaultAsync();

if (godfather2update is not null)
{
    godfather2update.RatingScore = 9.9;

    Console.WriteLine($"The Godfather Part II was updated on the db: {(await context.SaveChangesAsync() > 0 ? "True" : "False")}");
}


Console.WriteLine("7. Disable Tracking");

var queryEnabledTracking = await context.Films.SelectMany(f => f.Actors).ToListAsync();

Console.WriteLine($"Enabled Tracked objects: {context.ChangeTracker.Entries().Count()}");

context.ChangeTracker.Clear();

var queryDisabledTracking = await context.Films.AsNoTracking().SelectMany(f => f.Actors).ToListAsync();

Console.WriteLine($"Disabled Tracked objects: {context.ChangeTracker.Entries().Count()}");


Console.ReadLine();