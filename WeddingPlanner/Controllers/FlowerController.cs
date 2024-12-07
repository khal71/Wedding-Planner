using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlannerApplication;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.Controllers
{
    [Route("/flower")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FlowerController : Controller
    {
        private readonly IFlowerService _service;
        public FlowerController(IFlowerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flower>>> GetAll()
        {
            var flowers = await _service.ListAsync();
            if (flowers != null)
            {
                return Ok(flowers);
            }
            return BadRequest("There are no Flowers listed.");
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Flower>> AddFlower([FromBody] Flower flower)
        {
            if (!ModelState.IsValid)
               {
                  return  BadRequest("Failed to add flower.");
            }
            var res = await _service.AddAsync(flower);
            if (res.IsSuccess)
            {
                return Ok();
            }
            return BadRequest("Failed to add flower.");

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Flower>> GetById([FromRoute] int id)
        {
            var flower = await _service.GetByIdAsync(id);
            if (flower.IsSuccess)
            {
                return Ok(flower.Model);
            }
            return BadRequest("There is no such flower.");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Flower>> Delete([FromRoute] int id)
        {
            var res = await _service.DeleteAsync(id);
            if (res.IsSuccess)
            {
                return Ok(res.Model);
            }
            return BadRequest("There are no such flower to delete.");
        }

        [HttpPut]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Flower>> Update(Flower flower)
        {
            var res = await _service.UpdateAsync(flower.Id, flower);
            if (res.IsSuccess && res.Model != null)
            {
                return res.Model;
            }
            return BadRequest("There is no such flower to update.");
        }
            
        
    }
}
