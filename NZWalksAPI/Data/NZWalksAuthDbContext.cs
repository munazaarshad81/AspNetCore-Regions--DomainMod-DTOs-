using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace NZWalksAPI.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {

        
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "86f88f8f-3907-442d-bc62-7aa692321e3a";
            var writerRoleId = "bcc90c53-695d-4d75-a6d4-38c79f747483";

            // Creating Roles For Reader and Writer
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="READER"
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    Name="Writer",
                    ConcurrencyStamp=writerRoleId,
                    NormalizedName="WRITER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
