using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using System.Threading.Tasks;

namespace WeddingPlanner.RazorPages.Pages.Food
{
    public class ViewFoodModel : PageModel
    {
        private readonly FoodService _foodService;
        public ViewFoodModel(FoodService foodService)
        {
            _foodService = foodService;
        }

        public WeddingPlannerDomain.Entities.Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var food = await _foodService.GetFoodByIdAsync(id);
            if (food.Id == null)
            {
                return NotFound();
            }
            Food = food;
            return Page();
        }
    }
}

