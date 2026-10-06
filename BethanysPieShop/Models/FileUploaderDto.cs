using System.Text.Json.Serialization;

namespace BethanysPieShop.Models
{
    public class FileUploaderDto
    {
        public string Name { get; set; } = string.Empty;
        public List<int> NotificationPreferences { get; set; } = [];
        public List<IFormFile>? Files { get; set; }
    }
}
