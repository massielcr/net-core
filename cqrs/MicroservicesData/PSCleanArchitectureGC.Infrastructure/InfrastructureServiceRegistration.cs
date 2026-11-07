using Microsoft.Extensions.DependencyInjection;
using PSCleanArchitectureGC.Application.Contracts.Infrastructure;
using PSCleanArchitectureGC.Infrastructure.Mail;

namespace PSCleanArchitectureGC.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
