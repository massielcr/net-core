using Microsoft.Extensions.Logging;
using PSCleanArchitectureGC.Application.Contracts.Infrastructure;
using PSCleanArchitectureGC.Application.Models.Mail;

namespace PSCleanArchitectureGC.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task<bool> SendEmail(Email email)
        {
            _logger.LogInformation(
                "Email notification: recipient {Recipient}; subject {Subject}; body {Body}",
                email.To,
                email.Subject,
                email.Body);

            return Task.FromResult(true);
        }
    }
}
