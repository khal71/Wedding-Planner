using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using WeddingPlanner.RazorPages.Pages.Auth;

namespace WeddingPlanner.RazorPages.Pages.Food
{
    public class CreateFoodModel : PageModel
    {
        private readonly FoodService _foodService;
        private readonly SessionManager _sessionManager;
        public CreateFoodModel(FoodService foodService, SessionManager sessionManager)
        {
            _foodService = foodService;
            _sessionManager = sessionManager;

        }

        [BindProperty]
        public WeddingPlannerDomain.Entities.Food food { get; set; }

        [BindProperty]
        public bool isAdmin {  get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public void OnGet()
        {
            isAdmin = _sessionManager.IsAdmin;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            food.Id = 0; 

            if (ImageFile !=null)
            {
                var allowedExtentions = new[] { "image/jpeg\", \"image/png\", \"image/gif" };
                if (!allowedExtentions.Contains(ImageFile.ContentType))
                {
                    ModelState.AddModelError("ImageFile", "please uplode a valid image file( JPEG, PNG, GIF).");
                    return Page(); 

                }

                using (var memoryStream= new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    food.ImageData = memoryStream.ToArray();
                }
            }
            var res = await _foodService.AddFoodAsync(food);
            if (res == true)
            {
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }

        
    }
}
