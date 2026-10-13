namespace PSModelingRelationshipsFS.Web.Entities
{
    public class CustomerPreference
    {
        public UIPreferences UI { get; set; } = new();
        public NotificationPreferences Notifications { get; set; } = new();
        public string[] FavoriteCategories { get; set; } = [];
    }
}
