using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeddingPlanner.RazorPages.Pages.Auth
{
    public class SessionManager
    {
        private readonly IConfiguration _configuration;
        private JwtSecurityToken _jwtToken;
        public string stringToken {get; private set;}
        public SessionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Method to set the token, e.g., retrieved from localStorage on the client side
        public void SetToken(string token)
        {
            stringToken = token;
            _jwtToken = DecodeToken(token);
        }

        // Decode the JWT token
        private JwtSecurityToken DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);

            try
            {
                var validationParams = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]))
                };

                tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);
                return (JwtSecurityToken)validatedToken;
            }
            catch
            {
                // Handle invalid token
                return null;
            }
        }

        // Check if the user is authenticated
        public bool IsAuthenticated => _jwtToken != null;

        // Check if the user has an "Admin" role
        public bool IsAdmin => _jwtToken?.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin") == true;

        // Retrieve a specific claim value by claim type
        public string GetClaim(string claimType)
        {
            return _jwtToken?.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

        // Clear token (log out the user)
        public void ClearToken()
        {
            stringToken = null;
            _jwtToken = null;

        }
    }
}
