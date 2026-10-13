namespace PSModelingRelationshipsFS.Web.Entities
{
    public abstract class ShippableProduct : Product
    {
        public bool RequiresSignature { get; set; }
    }
}
