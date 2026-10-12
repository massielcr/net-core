namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Suplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // many:many
        public List<ProductSuplier> Products { get; set; } = [];
    }
}
