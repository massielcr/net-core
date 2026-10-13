namespace PSModelingRelationshipsFS.Web.Entities
{
    public class PhysicalProduct : ShippableProduct
    {
        public decimal WeigthKg { get; set; }
        public Dimensions Dimensions { get; set; } = null!;
        public string? Manufacturer { get; set; }
    }
}
