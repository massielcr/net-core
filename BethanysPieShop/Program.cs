using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot _configurationRoot = builder.Configuration;

IServiceCollection services = builder.Services;

ConfigureServices(services);

var app = builder.Build();

IWebHostEnvironment webHostEnvironment = app.Environment;

Configure(app, webHostEnvironment);



void ConfigureServices(IServiceCollection services)
{
    services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

    services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
    services.AddOpenApi();

    services.AddDbContext<AppDbContext>(options =>
                                         options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

    services.AddTransient<IPieRepository, PieRepository>();
    services.AddTransient<ICategoryRepository, CategoryRepository>();
}

// Configure the HTTP request pipeline.
void Configure(WebApplication app, IWebHostEnvironment webHostEnvironment)
{
    if (webHostEnvironment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Bethany's Pie Shop API V1");
        });
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    DbInitializer.Seed(app);



    app.MapGet("api/minimal/pie", (IPieRepository pieRepository) => {
        var pies = pieRepository.GetAll().ToList();
        return Results.Ok(pies);
    });


    app.Run();    
}