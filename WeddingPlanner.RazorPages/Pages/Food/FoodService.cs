using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Food
{
    public class FoodService
    {
        private readonly HttpClient _httpClient;
        public readonly SessionManager _sessionManager; 

        public FoodService(IHttpClientFactory httpClientFactory, SessionManager sessionManager)
        {
            _httpClient = httpClientFactory.CreateClient("ApiHttpClinet"); 
            _sessionManager = sessionManager;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionManager.stringToken);

        }
        public async Task<List<WeddingPlannerDomain.Entities.Food>> GetAllFoodsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<WeddingPlannerDomain.Entities.Food>>("./food");
        }

        public async Task<WeddingPlannerDomain.Entities.Food> GetFoodByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<WeddingPlannerDomain.Entities.Food>($"/food/{id}");

        }

        public async Task<bool> AddFoodAsync(WeddingPlannerDomain.Entities.Food food)
        {
            var res = await _httpClient.PostAsJsonAsync("/food", food); 
            return res.IsSuccessStatusCode;
        }
        public async Task UpdateFoodAsync(WeddingPlannerDomain.Entities.Food food)
        {
            await _httpClient.PutAsJsonAsync($"/food", food); 

        }
        public async Task<bool> DeleteFoodAsync(int id)
        {
            var res = await _httpClient.DeleteAsync($"/food/{id}"); 
            return res.IsSuccessStatusCode;
        }
    }
}
