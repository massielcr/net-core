namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public required Money Price { get; set; }
    }
}
