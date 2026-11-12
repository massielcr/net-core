
using BethanysPieShop.Services.Validator.Rules;

namespace BethanysPieShop.Services.Validator
{
    public class FileValidator(List<IFileValidationRule> rules) : IFileValidator
    {
        public bool IsValid(List<IFormFile>? files)
        {
            return files?.All(IsValid) ?? false;
        }

        public bool IsValid(IFormFile? file)
        {
            return rules.All(rule => rule.IsValid(file));
        }
    }
}
