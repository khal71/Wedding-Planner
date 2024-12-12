using System.Net.Http.Headers;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Users
{
    public class UserServiceR
    {
        
       
            private readonly HttpClient _httpClient;
            public readonly SessionManager _sessionManager;
            public UserServiceR(IHttpClientFactory httpClientFactory, SessionManager sessionManager)
            {
                _httpClient = httpClientFactory.CreateClient("ApiHttpClient");
                _sessionManager = sessionManager;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionManager.stringToken);
            }
            public async Task<List<User>> GetAllUsersAsync()
            {

                return await _httpClient.GetFromJsonAsync<List<User>>("./user");

            }

            public async Task<User> GetUserByIdAsync(int id)
            {
                return await _httpClient.GetFromJsonAsync<User>($"/user/{id}");
            }

            

            public async Task UpdateUserAsync(User user)
            {
                await _httpClient.PutAsJsonAsync($"/user", user);
            }

            public async Task<bool> DeleteUserAsync(int id)
            {
                var res = await _httpClient.DeleteAsync($"/user/{id}");
                return res.IsSuccessStatusCode;
            }
        }
    }

