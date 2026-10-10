namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //one-to-many
        public List<Order> Orders { get; set; } = [];
    }
}
