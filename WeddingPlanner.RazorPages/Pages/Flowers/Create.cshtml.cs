using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
    
        public class CreateFlowerModel : PageModel
        {
            private readonly IFlowerService _flowerService;

           
            public CreateFlowerModel(IFlowerService flowerService)
            {
                _flowerService = flowerService;
            }

            [BindProperty]
            public Flower Flower { get; set; }

            
            public void OnGet()
            {
            }

          
            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                
                await _flowerService.AddAsync(Flower);

                
                return RedirectToPage("Index");
            }
        }
    }

