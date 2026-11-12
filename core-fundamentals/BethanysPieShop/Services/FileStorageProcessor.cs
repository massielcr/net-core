using BethanysPieShop.Models;
using BethanysPieShop.Services.Storages;

namespace BethanysPieShop.Services
{
    public class FileStorageProcessor(IFileStorageService fileStorageService) : IFileProcessor
    {
        private IFileProcessor? _nextProcessor;

        private readonly IFileStorageService _fileStorageService = fileStorageService;

        public void SetNext(IFileProcessor nextProcessor)
        {
            _nextProcessor = nextProcessor;
        }

        public bool ProcessFile(FileUploaderDto fileUploaderDto)
        {
            return _nextProcessor == null || _nextProcessor.ProcessFile(fileUploaderDto);
        }        
    }
}
