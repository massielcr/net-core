using BethanysPieShop.Services.Notifications.Models;
using System.Threading.Tasks;

namespace BethanysPieShop.Services.Notifications
{
    public class NotificationObservable(IEnumerable<INotificationObserver> observers) : INotificationObservable
    {
        private readonly List<INotificationObserver> _observers = [.. observers];

        public async Task NotifyObserversAsync(UserPreferences userPreferences, NotificationRequest notificationRequest)
        {
            var tasks = _observers.Select(o => o.NotifyAsync(userPreferences, notificationRequest));
            await Task.WhenAll(tasks); // Runs Email and SMS in parallel
        }
    }
}
