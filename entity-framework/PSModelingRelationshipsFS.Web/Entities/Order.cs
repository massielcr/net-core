namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        //FK + relationship from 1:many
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public Address ShipTo { get; set; } = new();

        // 1:many
        public List<OrderLine> Lines { get; set; } = [];

        // optional FK relationship from 1:many
        public SalesAgent? SalesAgent { get; set; }

        public decimal Total => Lines.Sum(l => l.UnitPrice.Amount * l.Quantity);
    }
}
