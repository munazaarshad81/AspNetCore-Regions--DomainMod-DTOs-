using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFiles;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            
            // Mapping DTO to Domain model
            var walkDomainModel = mapper.Map<Models.Domains.Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            // Mapping Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
        // Get Walks
        //Get: api / walks? filterOn=Name&filterQuery=Track &sortby=Name&isAscending=true&PageNumber=1&Pagesize=10
        [HttpGet] // Get all
        public async Task<IActionResult> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, int pageSize=1000) //Filtering parameters
        {
            if (ModelState.IsValid)
            {
                // Get Domain model from database
                var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy, isAscending ?? true, pageNumber , pageSize);

                throw new Exception("This is a new exception for testing purpose");
                // Mapping Domain model to DTO
                return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
            }
            return BadRequest(ModelState);
        }

        // Get by ID 
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {

            //Mapping DTO to Domain model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);//Destination<>,then Source()

            await walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));


        }
    




        }

    }


