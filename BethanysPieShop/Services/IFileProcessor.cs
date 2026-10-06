using BethanysPieShop.Models;

namespace BethanysPieShop.Services
{
    public interface IFileProcessor
    {
        public void SetNext(IFileProcessor nextProcessor);
        public bool ProcessFile(FileUploaderDto file);        
    }
}
