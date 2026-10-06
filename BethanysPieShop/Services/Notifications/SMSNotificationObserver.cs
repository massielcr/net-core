using BethanysPieShop.Services.Notifications.Models;
using BethanysPieShop.Services.Notifications.SMS;

namespace BethanysPieShop.Services.Notifications
{
    public class SMSNotificationObserver(ISMSSender smsSender) : INotificationObserver
    {
        public async Task NotifyAsync(UserPreferences userPreferences, NotificationRequest notificationRequest)
        {
            if (!userPreferences.SMSEnabled)
                return;

            var text = notificationRequest.SmsMessage ?? notificationRequest.FallbackMessage ?? "You have a new alert.";

            await smsSender.SendSMSAsync(userPreferences.Phone, text);
        }
    }
}
