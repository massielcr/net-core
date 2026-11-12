namespace BethanysPieShop.Services.Validator
{
    public interface IFileValidator
    {
        bool IsValid(List<IFormFile>? files);
        bool IsValid(IFormFile? file);
    }
}
