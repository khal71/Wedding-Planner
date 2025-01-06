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
        public async Task<List<WeddingPlannerDomain.Flower>> GetAllFlowersAsync()
        {
            
            return await _httpClient.GetFromJsonAsync<List<WeddingPlannerDomain.Flower>>("./flower");
            
        }

        public async Task<WeddingPlannerDomain.Flower> GetFlowerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<WeddingPlannerDomain.Flower>($"/flower/{id}");
        }

        public async Task<bool> AddFlowerAsync(WeddingPlannerDomain.Flower flower)
        {
           
           var res = await _httpClient.PostAsJsonAsync("/flower", flower);
            return res.IsSuccessStatusCode;
        }

        public async Task UpdateFlowerAsync(WeddingPlannerDomain.Flower flower)
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
