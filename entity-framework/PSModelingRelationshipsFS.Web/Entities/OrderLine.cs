namespace PSModelingRelationshipsFS.Web.Entities
{
    public class OrderLine
    {
        //FK + relationship
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        //FK + relationship
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
        public required Money UnitPrice { get; set; }
    }
}
