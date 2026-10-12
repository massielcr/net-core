namespace PSModelingRelationshipsFS.Web.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // skip many: many
        public List<Product> Products { get; set; } = [];
    }
}
