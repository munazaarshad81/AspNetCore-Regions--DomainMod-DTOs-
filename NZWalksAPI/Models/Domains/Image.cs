using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Domains
{
    public class Image
    {
        [NotMapped]
        public IFormFile? File { get; set; }

        public Guid Id { get; set; }

        // This will be saved in DB
        [Required]
        public string FileName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string FileExtension { get; set; } = string.Empty;

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; } = string.Empty;

    }
}
