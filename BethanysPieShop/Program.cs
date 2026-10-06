using BethanysPieShop.Models;
using BethanysPieShop.Services;
using BethanysPieShop.Services.Notifications;
using BethanysPieShop.Services.Notifications.BackgroundQueue;
using BethanysPieShop.Services.Notifications.Email;
using BethanysPieShop.Services.Notifications.SMS;
using BethanysPieShop.Services.Storages;
using BethanysPieShop.Services.Validator;
using BethanysPieShop.Services.Validator.Rules;
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

    services.AddScoped<IPieRepository, PieRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();



    #region File Uploader Validator
    // Validation Rules
    builder.Services.AddTransient<IFileValidationRule, NullFileValidationRule>();
    builder.Services.AddTransient<IFileValidationRule, MaxFileSizeValidationRule>();
    builder.Services.AddTransient<IFileValidationRule, FileExtensionValidationRule>();
    builder.Services.AddTransient<IFileValidationRule, FileMimeTypeValidationRule>();

    builder.Services.AddTransient<IFileValidator, FileValidator>();

    builder.Services.AddTransient<FileValidatorBuilder>();

    #endregion


    #region File Uploader Notification Services

    // 1. Settings & Infrastructure
    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
    builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.AddTransient<ISMSSender, SMSSender>();

    // 2. The Background Queue (Must be Singleton)
    // This allows the API to "hand off" work to the background worker.
    builder.Services.AddSingleton<INotificationQueue, NotificationQueue>();

    // 3. Register ALL Observers under the same interface
    // The DI container will gather these into an IEnumerable<INotificationObserver>.
    builder.Services.AddTransient<INotificationObserver, EmailNotificationObserver>();
    builder.Services.AddTransient<INotificationObserver, SMSNotificationObserver>();

    // 4. Register the Observable (Subject)
    // It will automatically receive the list of observers via its constructor.
    builder.Services.AddTransient<INotificationObservable, NotificationObservable>();

    // 5. Register the Background Service (The Worker)
    // This class runs for the entire lifetime of the app, watching the queue.
    builder.Services.AddHostedService<NotificationWorker>();
       
    services.AddTransient<FileNotificationProcessor>();

    #endregion

    


    services.AddTransient<FileValidatorProcessor>();
    services.AddTransient<FileStorageProcessor>();
    services.AddTransient<FileProcessorBuilder>();
    services.AddTransient<IFileStorageService, EFFileStorageService>();
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