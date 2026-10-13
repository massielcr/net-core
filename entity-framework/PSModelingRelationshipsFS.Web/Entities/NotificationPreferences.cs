namespace PSModelingRelationshipsFS.Web.Entities
{
    public class NotificationPreferences
    {
        public bool EmailEnabled { get; set; } = true;
        public bool SmsEnabled { get; set; }
        public bool MarketingOptIn { get; set; }
    }
}