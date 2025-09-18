namespace NZWalksAPI.Models.DTOs
{
    public class AddRegionRequestDto //this class is used to receive data when adding a new region
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }




    }
}
