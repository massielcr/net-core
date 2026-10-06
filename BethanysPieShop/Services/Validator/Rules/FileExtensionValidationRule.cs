
namespace BethanysPieShop.Services.Validator.Rules
{
    public class FileExtensionValidationRule(string[] permittedExtensions) : IFileValidationRule
    {
        public bool IsValid(IFormFile? file)
        {
            var extension = Path.GetExtension(file?.FileName)?.ToLowerInvariant();
            return !string.IsNullOrEmpty(extension) && permittedExtensions.Contains(extension);
        }
    }
}
