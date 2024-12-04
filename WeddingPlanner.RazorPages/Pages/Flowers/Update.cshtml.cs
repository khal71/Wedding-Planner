using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
  
    
        public class UpdateFlowerModel : PageModel
        {
        private readonly FlowerService _flowerService;

        public UpdateFlowerModel(FlowerService flowerService)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _flowerService.UpdateFlowerAsync(Flower);

            return RedirectToPage("Index");
        }
    }


        }
    

