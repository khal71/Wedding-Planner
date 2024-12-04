

using System.Security.Cryptography;
using System.Text;
using WeddingPlanner.DTO;

namespace WeddingPlanner.RazorPages.Pages.Auth
{
    public class AuthService
    {

        private readonly HttpClient _httpClient;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiHttpClient");
        }

        public async Task<LoginResponse> LoginAdmin(LoginModelDTO admin)
        {
            admin.Password = AuthService.HashPassword(admin.Password);
            var response = await _httpClient.PostAsJsonAsync("./admin", admin);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResponse>();

            }
            return null;
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert hash byte array to hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }


        }
    }
}
