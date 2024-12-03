using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTOs;
using NZWalk.API.Repositories;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepo regionRepo;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepo regionRepo, IMapper mapper )
        {
            this.dbContext = dbContext;
            this.regionRepo = regionRepo;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // get data from db
            var regionsDomain = await regionRepo.GetAllAsync();

            //map domain model to DTOs
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);    

            //return DTOs
            return Ok(regionsDto);
        }

        // get single region (by id)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepo.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomain)); 
            
        }

        //POST to create a new region 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // convert DTO to domain model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //USE domain model to create region 
            regionDomainModel = await regionRepo.CreateAsync(regionDomainModel);

            //map domain model back to  dto

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id },  regionDto );
        }


        //update region (put)
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepo.UpdateAsync(id, regionDomainModel);
        
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var regionDomainModel = await regionRepo.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }
    }
}
