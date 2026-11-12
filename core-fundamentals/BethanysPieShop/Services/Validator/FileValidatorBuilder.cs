using BethanysPieShop.Services.Validator.Rules;

namespace BethanysPieShop.Services.Validator
{
    public class FileValidatorBuilder(IServiceProvider serviceProvider)
    {
        private readonly List<IFileValidationRule> _rules = [];

        public FileValidatorBuilder AddRule<TRule>(params object[] parameters) where TRule : IFileValidationRule
        {
            // Resolves DI services (like AI clients) AND injects your custom parameters
            var rule = ActivatorUtilities.CreateInstance<TRule>(serviceProvider, parameters);
            _rules.Add(rule);
            return this;
        }

        public FileValidatorBuilder RequireFile() => AddRule<NullFileValidationRule>();

        public FileValidatorBuilder MaxSize(long bytes) => AddRule<MaxFileSizeValidationRule>(bytes);

        public FileValidatorBuilder FileExtensions(string[] permittedExtensions) => AddRule<FileExtensionValidationRule>(permittedExtensions);

        public FileValidatorBuilder MimeTypes(string[] permittedMimeTypes) => AddRule<FileMimeTypeValidationRule>(permittedMimeTypes);

        public IFileValidator Build() => new FileValidator(_rules);
    }
}
