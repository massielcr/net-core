namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        //FK + relationship
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public Address ShipTo { get; set; } = new();

        //one-to-many
        public List<OrderLine> Lines { get; set; } = [];
        public decimal Total => Lines.Sum(l => l.UnitPrice.Amount * l.Quantity);
    }
}
