namespace NZWalksAPI.Models.Domains
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        // Navigation properties(let you navigate the relationships between tables (entities) in your database.)
        public Region Region { get; set; } // one to one connection with walks
        public Difficulty Difficulty { get; set; } // one to one connection with walks

    }
}
