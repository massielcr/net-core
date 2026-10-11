namespace PSModelingRelationshipsFS.Web.Entities
{
    public class SalesAgent
    {
        public int Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;

        // 1:many
        public List<Order> Orders { get; set; } = [];
    }
}
