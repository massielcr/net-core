using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSGettingStartedSW.DataContext;
using System.Reflection;

Console.WriteLine("Starting App..");


var builder = Host.CreateApplicationBuilder();

builder.Services.AddDbContext<FilmDbContext>();

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());



var app = builder.Build();

var context = app.Services.GetRequiredService<FilmDbContext>();

context.Database.EnsureCreated();

var qry = context.Films
                 .Where(f => f.Mpaa == "R")
                 .OrderByDescending(f => f.Year)
                 .Select(f => f.Title);

Console.WriteLine($"Titles: {string.Join(", ", qry.ToArray())}");



Console.ReadLine();