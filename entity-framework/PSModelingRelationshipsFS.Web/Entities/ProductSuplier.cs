namespace PSModelingRelationshipsFS.Web.Entities
{
    public class ProductSuplier
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int SuplierId { get; set; }
        public Suplier Suplier { get; set; } = null!;

        public decimal ContractPrice { get; set; }
        public int LeadTimeDays { get; set; }
        public bool IsPreferred { get; set; }
    }
}