using BethanysPieShop.Services.Notifications.Email;
using BethanysPieShop.Services.Notifications.Models;

namespace BethanysPieShop.Services.Notifications
{
    public class EmailNotificationObserver(IEmailSender emailSender) : INotificationObserver
    {
        public async Task NotifyAsync(UserPreferences userPreferences, NotificationRequest notificationRequest)
        {
            if (!userPreferences.EmailEnabled)
                return;

            await emailSender.SendEmailAsync(
                to: userPreferences.Email,
                subject: notificationRequest.EmailSubject ?? "New Notification",
                body: notificationRequest.EmailBodyHtml ?? notificationRequest.FallbackMessage ?? "New Alert"
            );
        }
    }
}

