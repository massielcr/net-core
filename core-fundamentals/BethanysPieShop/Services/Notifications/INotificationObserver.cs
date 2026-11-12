using BethanysPieShop.Services.Notifications.Models;

namespace BethanysPieShop.Services.Notifications
{
    public interface INotificationObserver
    {
        Task NotifyAsync(UserPreferences userPreferences, NotificationRequest notificationRequest);
    }
}
