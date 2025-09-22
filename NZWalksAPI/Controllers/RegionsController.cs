using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFiles;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;
using System.Collections.Generic;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // This will make sure that all the endpoints in this controller are protected and only accessible to authenticated users.
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext dbContext;
        private IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        //GET ALL REGIONS
        //Get: https://localhost:portnumber/api/regions
        public async Task<IActionResult> GetAll() // Implementing Async programming
        {

            //1- Get Region Domain Model From Database
            var RegionsDomain = await regionRepository.GetAllAsync();

            ////2- Convert Domain to DTO
            //var regionDto = new List<Regiondto>();
            //foreach (var region in RegionsDomain)
            //{
            //    regionDto.Add(new Regiondto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl
            //    });
            //}
            // This line will Map Domain Model to Dto, and even from Dto to the client
            return Ok(mapper.Map<List<Regiondto>>(RegionsDomain));



        }
        //GET SINGLE REGION (Get Region By Id)
        // Get: https://localhost:portnumber/api/regions/{id}
        // 1- Get Region Domain Model From Database
        [HttpGet]
        [Route("id:Guid")]

        public async Task<IActionResult> GetById(Guid id)
        {
            // var region =dbContext.Regions.FirstOrDefault(x => x.Id == id);
            // var region = dbContext.Regions.Find(id);
            var regionsDomain = await dbContext.Regions.FindAsync(id);
            if (regionsDomain == null)
            {
                return NotFound();

            }
            //2- Convert Domain to DTO
            //var regionDto = new Regiondto()
            //{
            //    Id = regionsDomain.Id,
            //    Name = regionsDomain.Name,
            //    Code = regionsDomain.Code,
            //    RegionImageUrl = regionsDomain.RegionImageUrl
            //};
            //// Return the DTO to the client 
            //return Ok(regionsDomain);

            // Automapper will Convert DM to DTO and DTO back to the client
            return Ok(mapper.Map<Regiondto>(regionsDomain));
        }
        // POST - Client can Add a new region into the DB
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Converting DTO to Domain Model
            var regionDomain = mapper.Map<Region>(addRegionRequestDto); //Automapper
                                                                        //{

            //    Name = addRegionRequestDto.Name,
            //    Code = addRegionRequestDto.Code,
            //    RegionImageUrl = addRegionRequestDto.RegionImageUrl
            //};
            // Using the Domain Model to create region in the Database
            await regionRepository.CreateAsync(regionDomain);
            // to actually save the changes to the database
            await dbContext.SaveChangesAsync();

            // To Map domain model back to DTO (to send from DB to the client)
            var regionDto = mapper.Map<Regiondto>(regionDomain); //Automapper

            return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

    

        //update region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {    //map domain model to DTO
          
            
                var regionDomain = mapper.Map<Region>(updateRegionRequestDto); //<> will have source and ()will have destination later
                                                                               //{
                                                                               //    Code = updateRegionRequestDto.Code,
                                                                               //    Name = updateRegionRequestDto.Name,
                                                                               //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                                                                               //};
                                                                               //Check if region exists in the DB
                regionDomain = await regionRepository.GetByIdAsync(id);
                if (regionDomain == null)
                {
                    return NotFound();
                }
                // Conveert Domain Model back to DTO
                //var regionDto = new Regiondto()
                //{
                //    Id = regionDomain.Id,
                //    Name = regionDomain.Name,
                //    Code = regionDomain.Code,
                //    RegionImageUrl = regionDomain.RegionImageUrl

                //};
                return Ok(mapper.Map<UpdateRegionRequestDto>(regionDomain));
            }
           

        
           

        //DELTETE Region
        // DELETE: https://localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route ("{id:guid}")]

        public async Task<IActionResult> delete([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // Map Domain Model back to DTO
            //var regionDto = new Regiondto()
            //{
            //    Id = regionDomain.Id,
            //    Name = regionDomain.Name,
            //    Code = regionDomain.Code,
            //    RegionImageUrl = regionDomain.RegionImageUrl

            //};

            //return Ok(regionDto);

            return Ok(mapper.Map<Regiondto>(regionDomain));

        }
        




    }
}    
