using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeddingPlanner.RazorPages.Pages.Food
{
    public class UpdateFoodModel : PageModel
    {
        private readonly FoodService _foodService;

        public UpdateFoodModel(FoodService foodService)
        {
            _foodService = foodService;
        }
        [BindProperty]
        public WeddingPlannerDomain.Entities.Food food { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            food = await _foodService.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ImageFile != null)
            {
                var allowedExtension = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedExtension.Contains(ImageFile.ContentType))
                {
                    ModelState.AddModelError("ImageFile", "please upload a vaild image file (JPEG, PNG, GIF).");
                    return Page();
                }
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    food.ImageData = memoryStream.ToArray();
                }
            }
            await _foodService.UpdateFoodAsync(food);
            return RedirectToPage("Index"); 
        }
    }
}
