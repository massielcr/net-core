namespace PSModelingRelationshipsFS.Web.Entities
{
    public class SubscriptionProduct : Product
    {
        public int BillingPeriodMonths { get; set; }
        public bool AutoRenews { get; set; }
        public DateOnly? TrialEndsOn { get; set; }
    }
}
