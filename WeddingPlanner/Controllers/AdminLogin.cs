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
    public class AdminLogin : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

       
        public AdminLogin(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> LoginAdmin([FromBody] Admin request)
        {
            
            var adminUser = await _userManager.FindByEmailAsync(request.Email);
            if (adminUser == null || !await _userManager.CheckPasswordAsync(adminUser, request.Password))
            {
                return BadRequest("Invalid username or password");
            }

            
            var isAdmin = await _userManager.IsInRoleAsync(adminUser, "Admin");
            if (!isAdmin)
            {
                return Unauthorized("Access denied. Only admins can log in here.");
            }

            var authClaims = new List<Claim>
        {
            new Claim("adminEmail", request.Email),
            new Claim("adminId", request.Id.ToString()),
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

