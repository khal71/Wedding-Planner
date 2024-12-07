using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
    public class FlowerService
    {
        private readonly HttpClient _httpClient;
        public readonly SessionManager _sessionManager;
        public FlowerService(IHttpClientFactory httpClientFactory, SessionManager sessionManager)
        {
            _httpClient = httpClientFactory.CreateClient("ApiHttpClient");
            _sessionManager = sessionManager;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionManager.stringToken);
        }
        public async Task<List<Flower>> GetAllFlowersAsync()
        {
            
            return await _httpClient.GetFromJsonAsync<List<Flower>>("./flower");
            
        }

        public async Task<Flower> GetFlowerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Flower>($"/flower/{id}");
        }

        public async Task<bool> AddFlowerAsync(Flower flower)
        {
           
           var res = await _httpClient.PostAsJsonAsync<Flower>("/flower", flower);
            return res.IsSuccessStatusCode;
        }

        public async Task UpdateFlowerAsync(Flower flower)
        {
            await _httpClient.PutAsJsonAsync($"/flower", flower);
        }

        public async Task<bool> DeleteFlowerAsync(int id)
        {
            var res=  await _httpClient.DeleteAsync($"/flower/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
