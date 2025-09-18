using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTOs;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        //GET ALL REGIONS
        //Get: https://localhost:portnumber/api/regions
        public IActionResult GetAll()
        {

            //1- Get Region Domain Model From Database
            var RegionsDomain = dbContext.Regions.ToList();

            //2- Convert Domain to DTO
            var regionDto = new List<Regiondto>();
            foreach (var region in RegionsDomain)
            {
                regionDto.Add(new Regiondto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            // Return the DTO to the client
            return Ok(RegionsDomain);

        }
        //GET SINGLE REGION (Get Region By Id)
        // Get: https://localhost:portnumber/api/regions/{id}
        // 1- Get Region Domain Model From Database
        [HttpGet]
        [Route("id:Guid")]

        public IActionResult GetById(Guid id)
        {
            // var region =dbContext.Regions.FirstOrDefault(x => x.Id == id);
            // var region = dbContext.Regions.Find(id);
            var regionsDomain = dbContext.Regions.Find(id);
            if (regionsDomain == null)
            {
                return NotFound();

            }
            //2- Convert Domain to DTO
            var regionDto = new Regiondto()
            {
                Id = regionsDomain.Id,
                Name = regionsDomain.Name,
                Code = regionsDomain.Code,
                RegionImageUrl = regionsDomain.RegionImageUrl
            };
            // Return the DTO to the client 
            return Ok(regionsDomain);
        }
        // POST - Client can Add a new region into the DB
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Converting DTO to Domain Model
            var regionDomain = new Region()
            {

                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            // Using the Domain Model to create region in the Database
            dbContext.Regions.Add(regionDomain);
            // to actually save the changes to the database
            dbContext.SaveChanges();

            // To Map domain model back to DTO (to send from DB to the client)
            var regionDto = new Regiondto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

        //update region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]

        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exists in the DB
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // Map the request DTO to Domain Model
            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            // Conveert Domain Model back to DTO
            var regionDto = new Regiondto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl

            };

            return Ok(regionDto);
        }

        //DELTETE Region
        // DELETE: https://localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route ("{id:guid}")]

        public IActionResult delete([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // Delete Region
            dbContext.Regions.Remove(regionDomain);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO
            var regionDto = new Regiondto()
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl

            };

            return Ok(regionDto);

        }
        




    }
}    
