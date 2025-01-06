using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;


namespace WeddingPlanner.RazorPages.Pages.Food
{
    public class DeleteFoodModel : PageModel
    {
        private readonly FoodService _foodService;

        public DeleteFoodModel(FoodService foodService)
        {
            _foodService = foodService;
        }

        [BindProperty]

        public WeddingPlannerDomain.Entities.Food food { get; set; }

        public async Task<IActionResult> OneGetAsync(int id)
        {
            food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var res = await _foodService.DeleteFoodAsync(id);
            if (res == true)
            {
                return RedirectToPage("/Index"); 
            }
            else
            {
                ModelState.AddModelError("", "failed to delete food");
                return Page();
            }
        }

    }
}
