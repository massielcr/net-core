namespace BethanysPieShop.Services.Validator.Rules
{
    public interface IFileValidationRule
    {
        bool IsValid(IFormFile? file);
    }
}
