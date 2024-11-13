using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers
{
    public class UserRegister : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRegister( UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }

        [HttpPost("user/register")]
        public async Task<ActionResult> RegisterUser([FromBody] User request)
        {
            var existingUser = await _userManager.FindByNameAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(" Email is already taken");
            }
            var newUser = new IdentityUser
            {
                
                Email = request.Email
            };
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                
                return BadRequest(result.Errors);
            }

            // create user manager 
            //step 1 find user from email.
            /* step 2 
            var existingUser = await _userManager.FindByNameAsync(request.Username);
             if (existingUser is not null)
               {
               return BadRequest("Username is already taken");
               }*/
            // step 3 
            // service or repo var newUser = new UserEntity             {                Id = Guid.NewGuid().ToString(),  // Generate a unique ID UserName = request.Username, Password = request.Password, // In a real app, hash the password! IsAdmin = request.IsAdmin // Set admin status based on the request };
            // call create user service


            return Ok("User created");

        }
    }
};