using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeddingPlanner.DTO;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.Controllers
{
    [Route("/admin")]
    [ApiController]
    [Produces("application/json")]
    public class AdminLogin : Controller
    {

        private readonly IAdminService _adminService;
        private readonly IConfiguration _configuration;

       
        public AdminLogin(IAdminService adminService, IConfiguration configuration)
        {
            _adminService = adminService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginModelDTO request)
        {
            
            var adminUser = await _adminService.FindByEmailAsync(request.Email);
            if (adminUser.Model == null )
            {
                return BadRequest("Invalid username or password");
            }
            var valid = _adminService.ValidatePasswordAsync(request.Password, adminUser.Model.Password);
            if (valid == false)
            {
                return BadRequest("Invalid username or password");
            }


            var authClaims = new List<Claim>
        {
            new Claim("adminEmail", adminUser.Model.Email),
            new Claim("adminId", adminUser.Model.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin")  
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
}

