using BethanysPieShop.Services.Notifications.Models;

namespace BethanysPieShop.Services.Notifications
{
    public interface INotificationObservable
    {
        Task NotifyObserversAsync(UserPreferences userPreferences, NotificationRequest notificationRequest);
    }
}
