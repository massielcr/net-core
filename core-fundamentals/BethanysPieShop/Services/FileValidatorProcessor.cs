using BethanysPieShop.Models;
using BethanysPieShop.Services.Validator;
using Microsoft.IdentityModel.Tokens;

namespace BethanysPieShop.Services
{
    public class FileValidatorProcessor(FileValidatorBuilder fileValidatorBuilder) : IFileProcessor
    {
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly string[] _permittedMimeTypes = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _maxFileSize = 2 * 1024 * 1024;

        private IFileProcessor? _nextProcessor;

        public void SetNext(IFileProcessor nextProcessor)
        {
            _nextProcessor = nextProcessor;
        }

        public bool ProcessFile(FileUploaderDto fileUploaderDto)
        {
            var fileValidator = fileValidatorBuilder
                                    .RequireFile()
                                    .MaxSize(_maxFileSize)
                                    .FileExtensions(_permittedExtensions)
                                    .MimeTypes(_permittedMimeTypes)
                                    .Build();



            if (fileValidator.IsValid(fileUploaderDto.Files))
            {
                return _nextProcessor == null || _nextProcessor.ProcessFile(fileUploaderDto);
            }  

            return false;
        }      
    }
}
