using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{

    public class DeleteFlowerModel : PageModel
    {
        private readonly FlowerService _flowerService;

        public DeleteFlowerModel(FlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [BindProperty]
        public Flower Flower { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Flower = await _flowerService.GetFlowerByIdAsync(id);
            if (Flower == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var res = await _flowerService.DeleteFlowerAsync(id);
           
            if (res == true)
            {
                
                return Page(); // Redirect after deletion
            }
            else
            {
                // Handle failure
                ModelState.AddModelError("", "Failed to delete the flower.");
                return Page();
            }

        }
    }

}

