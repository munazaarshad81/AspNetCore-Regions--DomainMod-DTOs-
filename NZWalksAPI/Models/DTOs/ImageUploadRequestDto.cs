using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.DTOs
{
    public class ImageUploadRequestDto
    {

        [Required]
        [NotMapped]
        public  IFormFile File { get; set; }
        [Required]
        [NotMapped]
        public string FileName { get; set; }
        public string? Description { get; set; }
    }
}
