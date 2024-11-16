using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers
{
    public class UserLogin : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
      
       
        public UserLogin(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("user/login")]
        public async Task<ActionResult> LoginUser([FromBody] User request)
        {
            var user = await _userService.FindByEmailAsync(request.Email);
            if (user == null ||  _userService.ValidatePasswordAsync(request.Password, user.Model.Password))
            {
                return BadRequest("Invalid username or password");
            }
            
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
