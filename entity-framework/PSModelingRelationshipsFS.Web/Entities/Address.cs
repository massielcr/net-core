namespace PSModelingRelationshipsFS.Web.Entities
{
    public sealed class Address
    {
        public string Street { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string CountryCode { get; init; } = string.Empty;
    }
}
