using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers
{
    [Microsoft.AspNetCore.Components.Route("/Location")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll()
        {
            var locations = await _locationService.ListAsync();
            if (locations != null)
            {
                return Ok(locations);

            }
            return BadRequest("There are no locations listed ");
        }
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Location>> AddLocation([FromRoute] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("failed to add location"); 
            }
            var res = await _locationService.AddAsync(location);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return BadRequest("failed to add location"); 
        }
        [HttpGet("{id")]
        public async Task<ActionResult<Location>> GetById([FromRoute] int id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location.IsSuccess)
            {
                return Ok(location.Model);

            }
            return BadRequest("there is no such location"); 
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Location>> Delete([FromRoute] int id)
        {
            var res = await _locationService.DeleteAsync(id);
            if (!res.IsSuccess)
            {
                return Ok(res.Model);
            }
            return BadRequest("there are no such location to delete"); 

        }
        [HttpPut]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Location>> Update(Location location)
        {
            var res = await _locationService.UpdateAsync(location.Id, location);
            if (res.IsSuccess && res.Model != null)
            {
                return Ok(res.Model);
            }
            return BadRequest("there is no such location to update"); 
        }

    }

}

