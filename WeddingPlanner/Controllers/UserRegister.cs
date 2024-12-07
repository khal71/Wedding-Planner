using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.DTO;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers
{
    public class UserRegister : Controller
    {
        private readonly IUserService _userService;
        public UserRegister( IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost("user/register")]
        public async Task<ActionResult> RegisterUser([FromBody] LoginModelDTO request)
        {
            var existingUser = _userService.FindByEmailAsync(request.Email);
            if (existingUser.Result.Model != null)
            {
                return BadRequest(" Email is already taken");
            }
            var user = new User();
            user.Email = request.Email;
            user.Password = request.Password;
            var result = await _userService.AddAsync(user);

            if (!result.IsSuccess)
            {
                
                return BadRequest(result);
            }
   
            return Ok("User created");

        }
    }
};