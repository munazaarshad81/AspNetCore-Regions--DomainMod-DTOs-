using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTOs
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(1000)] //Applying data annotations for validation
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)] 
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

    }
}
