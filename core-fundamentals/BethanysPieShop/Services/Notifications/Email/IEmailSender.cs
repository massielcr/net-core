namespace BethanysPieShop.Services.Notifications.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);
    }
}
