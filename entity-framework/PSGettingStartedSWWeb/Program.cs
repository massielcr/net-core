using Microsoft.EntityFrameworkCore;
using PSGettingStartedSW.Web.Controllers;
using PSGettingStartedSW.Web.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FilmDbContext>(options =>
{
    string? configurationString = builder.Configuration.GetConnectionString("FilmDbWeb");

    options.UseSqlServer(configurationString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.MapActorEndpoints();


app.Run();
