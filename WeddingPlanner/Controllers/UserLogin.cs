using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers
{
    public class UserLogin : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _user;
      
       //inject user manager
        public UserLogin(IConfiguration configuration, UserManager<IdentityUser> user)
        {
            _configuration = configuration;
            _user = user;
        }

        [HttpPost("user/login")]
        public async Task<ActionResult> LoginUser([FromBody] User request)
        {
            var user = await _user.FindByEmailAsync(request.Email);
            if (user == null || !await _user.CheckPasswordAsync(user, request.Password))
            {
                return BadRequest("Invalid username or password");
            }
            // create user manager 
            //step 1 find user from email.
            /* step 2 
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
            return BadRequest("Invalid username or password");
            }*/
            // step 3 
            var authClaims = new List<Claim>
            {
                new Claim("userEmail", request.Email ),
                new Claim("userId", request.Id.ToString()),
                new Claim("isAdmin", request.IsAdmin.ToString())
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
              audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
              );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });

        }
    }
};
