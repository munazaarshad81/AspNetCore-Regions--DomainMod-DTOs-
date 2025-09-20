namespace NZWalksAPI.Models.DTOs
{
    public class WalkDto //this DTO contains all the properties that our DB will send back to the client
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
       
        public Regiondto Region{ get; set; } // to show the region details in the walk
        public DifficultyDto Difficulty { get; set; } // to show the difficulty details in the walk
    }
}
