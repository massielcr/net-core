namespace BethanysPieShop.Services.Notifications.SMS
{
    public interface ISMSSender
    {
        Task SendSMSAsync(string to, string message);
    }
}
