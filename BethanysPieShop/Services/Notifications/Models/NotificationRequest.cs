namespace BethanysPieShop.Services.Notifications.Models
{
    public record NotificationRequest(
        string? SmsMessage = null,    
        string? EmailSubject = null,   
        string? EmailBodyHtml = null,  
        string? FallbackMessage = null 
    );
}
