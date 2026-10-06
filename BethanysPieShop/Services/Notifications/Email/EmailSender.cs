using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BethanysPieShop.Services.Notifications.Email
{
    public class EmailSender(IOptions<SmtpSettings> settings) : IEmailSender
    {
        private readonly SmtpSettings _settings = settings.Value;

        public async Task SendEmailMailkitAsync(string to, string subject, string body, bool isHtml = true)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

            using var client = new SmtpClient();
            // Fix: Use SecureSocketOptions.Auto to be more resilient
            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Test", "test@localhost"));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(isHtml ? "html" : "plain") { Text = body };

            //using var client = new SmtpClient();

            //await client.ConnectAsync("127.0.0.1", 1025, SecureSocketOptions.None);

            //await client.SendAsync(message);
            //await client.DisconnectAsync(true);

            await message.WriteToAsync($@"C:\TestEmails\{Guid.NewGuid()}.eml");
        }        
    }
}
