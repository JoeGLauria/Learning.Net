using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }

        // get single region (by id)

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
            
        }



    }
}
