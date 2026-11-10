using PSCleanArchitectureGC.Application.Models.Mail;

namespace PSCleanArchitectureGC.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
