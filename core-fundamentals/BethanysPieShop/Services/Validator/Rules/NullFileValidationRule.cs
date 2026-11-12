namespace BethanysPieShop.Services.Validator.Rules
{
    public class NullFileValidationRule : IFileValidationRule
    {
        public bool IsValid(IFormFile? file)
        {
            return file != null && file.Length > 0;
        }
    }
}
