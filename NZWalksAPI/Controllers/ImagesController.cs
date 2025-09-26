using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase

    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadAsync([FromForm] ImageUploadRequestDto request)
        {
            // Validate first
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // Convert DTO to domain model
                var imageDomainModel = new Image
                {   File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = Path.GetFileNameWithoutExtension(request.File.FileName),
                    Description = request.Description,
                };

                // Save using repo
                await imageRepository.UploadAsync(imageDomainModel);


                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);

        }
           
        

        // Validation helper
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif",".jfif" };

            var extension = Path.GetExtension(request.File.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ImageFile", "Unsupported file extension. Allowed: .jpg, .jpeg, .png, .gif");
            }

            if (request.File.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "File size exceeds the 5MB limit.");
            }
        }
    }
}
