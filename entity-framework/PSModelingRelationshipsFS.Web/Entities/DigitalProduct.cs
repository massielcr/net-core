namespace PSModelingRelationshipsFS.Web.Entities
{
    public class DigitalProduct : Product
    {
        public string DownloadUrl { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public string? Manufacturer { get; set; }
    }
}
