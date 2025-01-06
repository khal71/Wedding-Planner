using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;


namespace WeddingPlanner.RazorPages.Pages.Location
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;
        public readonly SessionManager _sessionManager;

        public LocationService(IHttpClientFactory httpClientFactory, SessionManager sessionManager)
        {
            _httpClient = httpClientFactory.CreateClient("ApiHttpClinet");
            _sessionManager = sessionManager;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _sessionManager.stringToken);


        }
        public async Task<List<WeddingPlannerDomain.Entities.Location>> GetAllLocationAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<WeddingPlannerDomain.Entities.Location>>("./location");

        }
        public async Task<WeddingPlannerDomain.Entities.Location> GetLocationByIdAsnc(int id)
        {
            return await _httpClient.GetFromJsonAsync<WeddingPlannerDomain.Entities.Location>($"/location/{id}");
        }

        public async Task<bool> AddLocationAsync(WeddingPlannerDomain.Entities.Location location)
        {
            var res = await _httpClient.PostAsJsonAsync("/location", location);
            return res.IsSuccessStatusCode;

        }
        public async Task UpdateLocationAsync(WeddingPlannerDomain.Entities.Location location)
        {
            await _httpClient.PutAsJsonAsync($"/location", location);   
        }
        public async Task<bool> DeleteLocationAsync(int id)
        {
            var res = await _httpClient.DeleteAsync($"/location/{id}"); 
            return res.IsSuccessStatusCode;
        }
      
    }
}
