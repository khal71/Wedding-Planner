using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
    public class IndexFlowerModel : PageModel
    {
        private readonly IFlowerService _flowerService;

        public IndexFlowerModel(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }
        public List<Flower> Flowers { get; set; }

        public async Task OnGetAsync()
        {
            Flowers = await _flowerService.ListAsync();
        }
    }
}
