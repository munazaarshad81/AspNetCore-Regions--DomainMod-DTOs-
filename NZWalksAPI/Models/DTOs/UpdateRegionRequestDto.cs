using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTOs
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MaxLength(100)] //Applying data annotations for validation
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(3)]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
