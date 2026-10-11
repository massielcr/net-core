namespace PSModelingRelationshipsFS.Web.Entities
{
    public class CustomerProfile
    {
        // 1:1
        public Customer Customer { get; set; } = null!;
        public string LoyaltyTier { get; set; } = "Standard";
        public DateTime? DateOfBirthUtc { get; set; }
    }
}
