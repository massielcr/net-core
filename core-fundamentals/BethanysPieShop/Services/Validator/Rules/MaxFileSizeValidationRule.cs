
namespace BethanysPieShop.Services.Validator.Rules
{
    public class MaxFileSizeValidationRule(long maxFileSizeInBytes = 0) : IFileValidationRule
    {
        public bool IsValid(IFormFile? file)
        {
            return file?.Length <= maxFileSizeInBytes;
        }
    }
}
