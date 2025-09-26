using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):base(dbContextOptions)
        {
           
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                 Id = Guid.Parse("72c523de-27ac-4a30-b9f6-b89370250c3b"),
                 Name="Easy"
                },
                new Difficulty
                {
                    Id=Guid.Parse("c4821f51-bcb3-45dd-beaf-92fe0457460e"),
                    Name="Medium"
                },
                new Difficulty
                {
                    Id=Guid.Parse("2cb16262-dccf-40e6-971b-5a8ddc25c644") ,
                    Name="Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(difficulties);



            var regions = new List<Region>
            {
                new Region
                {
                    Id=Guid.Parse("7e6b548f-55cf-4936-90d8-dec24ef01410"),
                    Name="Aukland",
                    Code="AKL",
                    RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Auckland_Marina_%28273797355%29.jpeg/1920px-Auckland_Marina_%28273797355%29.jpeg"
                },
                new Region
                {
                    Id= Guid.Parse("1eed54c3-5ba5-44ae-b8b4-d5b2695537d3"),
                    Name="Wellington",
                    Code="WLG", 
                    RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d1/Wellington_City_from_Mt_Victoria_looking_towards_the_Basin_Reserve_and_City_Centre_%282%29.jpg/1920px-Wellington_City_from_Mt_Victoria_looking_towards_the_Basin_Reserve_and_City_Centre_%282%29.jpg"
                },
                new Region
                {
                    Id= Guid.Parse("f4d2b3f1-2dcb-4c8a-9c8a-2b5f6e3e8e3a"),
                    Name="Christchurch",
                    Code="CHC",
                    RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Christchurch_City_Centre_from_Bridge_of_Remembrance_looking_south_%282%29.jpg/1920px-Christchurch_City_Centre_from_Bridge_of_Remembrance_looking_south_%282%29.jpg"
                },
                new Region
                {
                    Id= Guid.Parse("d3b5c6e7-8f9a-4b1c-9d2e-3f4a5b6c7d8e"),
                    Name="Hamilton",
                    Code="HML",
                    RegionImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Hamilton_City_Centre_from_Hamilton_Lake_looking_north_%282%29.jpg/1920px-Hamilton_City_Centre_from_Hamilton_Lake_looking_north_%282%29.jpg"
                },
               };
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }    
}
