namespace BethanysPieShop.Services.Notifications.Models
{
    public record UserPreferences(string UserId, string Email, string Phone, bool EmailEnabled, bool SMSEnabled);
}
