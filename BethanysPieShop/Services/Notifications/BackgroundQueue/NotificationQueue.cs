using BethanysPieShop.Services.Notifications.Models;
using System.Threading.Channels;

namespace BethanysPieShop.Services.Notifications.BackgroundQueue
{
    public class NotificationQueue : INotificationQueue
    {
        private readonly Channel<(UserPreferences, NotificationRequest)> _queue = Channel.CreateUnbounded<(UserPreferences, NotificationRequest)>();
        public ValueTask QueueAsync(UserPreferences p, NotificationRequest r) => _queue.Writer.WriteAsync((p, r));
        public IAsyncEnumerable<(UserPreferences, NotificationRequest)> DequeueAllAsync(CancellationToken ct) => _queue.Reader.ReadAllAsync(ct);
    }
}
