using BethanysPieShop.Models;
using BethanysPieShop.Services;
using BethanysPieShop.Services.Notifications;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileProcessorBuilder _fileProcessorBuilder;

        public FileController(FileProcessorBuilder builder)
        {
            _fileProcessorBuilder = builder;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> FileUploader([FromForm] FileUploaderDto fileUploader)
        {
            if (fileUploader.Files == null || !fileUploader.Files.Any())
            {
                return BadRequest("No file uploaded.");
            }


            var fileProcessor = _fileProcessorBuilder
                                    .AddValidator()
                                    .AddStorage()
                                    .AddNotification()
                                    .Build();


            fileProcessor!.ProcessFile(fileUploader);





            var file = fileUploader.Files.First();

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();

            Response.Headers.Append("X-File-Name", file.FileName);
            Response.Headers.Append("Access-Control-Expose-Headers", "X-File-Name");


            return File(fileBytes, "image/gif");
        }
    }
}
