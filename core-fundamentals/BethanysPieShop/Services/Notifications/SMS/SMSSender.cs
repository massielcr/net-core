namespace BethanysPieShop.Services.Notifications.SMS
{
    public class SMSSender : ISMSSender
    {
        public Task SendSMSAsync(string to, string message)
        {
            // Here you would integrate with an SMS gateway API (e.g., Twilio, Nexmo)
            // For demonstration purposes, we'll just simulate sending an SMS
            Console.WriteLine($"Sending SMS to {to}: {message}");
            return Task.CompletedTask;
        }
    }
}
