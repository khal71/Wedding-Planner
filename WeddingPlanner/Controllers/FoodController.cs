using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers

{
    [Route("/Food")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FoodController : Controller
    {
        private readonly IFoodService _foodService;
        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetAll()
        {
            var food = await _foodService.ListAsync();
            if (food != null)
            {
                return Ok(food);
            }
            return BadRequest("there is no food listed"); 
        }
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Food>> AddFood([FromRoute] Food food)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("failed to add food"); 
            }
            var res = await _foodService.AddAsync(food);
            if (res.IsSuccess)
            {
                return Ok(res);
            }
            return BadRequest("failed to add food"); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetById([FromRoute] int id)
        {
            var food = await _foodService.GetByIdAsync(id);
            if (food.IsSuccess)
            {
                return Ok(food.Model);
            }
            return BadRequest("there is no such food"); 
        }
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Food>> Delete([FromRoute] int id)
        {
            var res = await _foodService.DeleteAsync(id);
            if (res.IsSuccess)
            {
                return Ok(res.Model);
            }
            return BadRequest("there is no such food to delete ");
        }
        [HttpPut]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Food>> Update(Food food)
        {
            var res = await _foodService.UpdateAsync(food.Id, food);
            if(res.IsSuccess && res.Model != null)
            {
                return res.Model; 
            }
            return BadRequest("there is no such food to update"); 
        }
    }
}
