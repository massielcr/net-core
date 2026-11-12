using BethanysPieShop.Services.Notifications.Models;

namespace BethanysPieShop.Services.Notifications.BackgroundQueue
{
    public interface INotificationQueue
    {
        ValueTask QueueAsync(UserPreferences userPreferences, NotificationRequest notificationRequest);
        IAsyncEnumerable<(UserPreferences, NotificationRequest)> DequeueAllAsync(CancellationToken ct);
    }
}
