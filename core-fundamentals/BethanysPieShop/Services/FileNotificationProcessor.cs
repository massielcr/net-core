using BethanysPieShop.Models;
using BethanysPieShop.Services.Notifications;
using BethanysPieShop.Services.Notifications.BackgroundQueue;
using BethanysPieShop.Services.Notifications.Models;
using System.Numerics;

namespace BethanysPieShop.Services
{
    public class FileNotificationProcessor : IFileProcessor
    {
        private readonly INotificationQueue _notificationQueue;

        private IFileProcessor? _nextProcessor;

        public FileNotificationProcessor(INotificationQueue notificationQueue)
        {
            _notificationQueue = notificationQueue;
        }

        public void SetNext(IFileProcessor nextProcessor)
        {
            _nextProcessor = nextProcessor;
        }

        public bool ProcessFile(FileUploaderDto fileUploaderDto)
        {
            UserPreferences userPreferences = new(
                UserId: "1",
                Email: "massiel.croca@gmail.com",
                Phone: "6478649765",
                EmailEnabled: fileUploaderDto.NotificationPreferences.Contains(1),
                SMSEnabled: fileUploaderDto.NotificationPreferences.Contains(2)
            );

            fileUploaderDto?.Files?.ForEach(f =>
            {
                NotificationRequest notificationRequest = new(
                    SmsMessage: $"You have a new file: {f.FileName}!",
                    EmailSubject: "New File Uploaded",
                    EmailBodyHtml: $"<h1>Success!</h1><p>The file <strong>{f.FileName}</strong> has been processed.</p>",
                    FallbackMessage: "New file notification."
                );

                _ = _notificationQueue.QueueAsync(userPreferences, notificationRequest);
            });
            

            return _nextProcessor == null || _nextProcessor.ProcessFile(fileUploaderDto); ;
        }        
    }
}
