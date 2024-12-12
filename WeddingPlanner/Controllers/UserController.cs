using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers
{
   
        [Route("/user")]
        [ApiController]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public class UserController : Controller
        {
            private readonly IUserService _service;
            public UserController(IUserService service)
            {
                _service = service;
            }

            [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
            {
                var users = await _service.ListAsync();
                if (users != null)
                {
                    return Ok(users);
                }
                return BadRequest("There are no users listed.");
            }

            

            [HttpGet("{id}")]
            public async Task<ActionResult<User>> GetById([FromRoute] int id)
            {
                var user = await _service.GetByIdAsync(id);
                if (user.IsSuccess)
                {
                    return Ok(user.Model);
                }
                return BadRequest("There is no such user.");
            }

            [HttpDelete("{id}")]
            
            public async Task<ActionResult<User>> Delete([FromRoute] int id)
            {
                var res = await _service.DeleteAsync(id);
                if (res.IsSuccess)
                {
                    return Ok(res.Model);
                }
                return BadRequest("There are no such user to delete.");
            }

            [HttpPut]
            [Authorize(Policy = "AdminOnly")]
            public async Task<ActionResult<User>> Update(User user)
            {
                var res = await _service.UpdateAsync(user.Id, user);
                if (res.IsSuccess && res.Model != null)
                {
                    return res.Model;
                }
                return BadRequest("There is no such user to update.");
            }


        }
    }

