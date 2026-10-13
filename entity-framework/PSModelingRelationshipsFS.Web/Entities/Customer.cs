namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Address ShippingAddress { get; set; } = new();
        public Address? BillingAddress { get; set; }

        // 1:1
        public CustomerProfile Profile { get; set; } = null!;

        // json
        public CustomerPreference Preferences { get; set; } = new();

        // 1:many
        public List<Order> Orders { get; set; } = [];
    }
}
