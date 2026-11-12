
using Microsoft.IdentityModel.Tokens;

namespace BethanysPieShop.Services.Validator.Rules
{
    public class FileMimeTypeValidationRule(string[] permittedMimeTypes) : IFileValidationRule
    {
        public bool IsValid(IFormFile? file)
        {
            var mimeType = file?.ContentType.ToLowerInvariant();
            return !string.IsNullOrEmpty(mimeType) && mimeType.StartsWith("image/");
        }
    }
}
