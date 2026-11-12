namespace BethanysPieShop.Services.Notifications.BackgroundQueue
{
    public class NotificationWorker(INotificationQueue queue, IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            await foreach (var (prefs, req) in queue.DequeueAllAsync(ct))
            {
                using var scope = scopeFactory.CreateScope();
                var observable = scope.ServiceProvider.GetRequiredService<INotificationObservable>();
                await observable.NotifyObserversAsync(prefs, req);
            }
        }
    }
}
