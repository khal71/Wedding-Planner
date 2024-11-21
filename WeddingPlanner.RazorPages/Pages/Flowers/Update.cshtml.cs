using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
  
    
        public class UpdateFlowerModel : PageModel
        {
            private readonly IFlowerService _flowerService;

            
            public UpdateFlowerModel(IFlowerService flowerService)
            {
                _flowerService = flowerService;
            }

         
            [BindProperty]
            public Flower Flower { get; set; }


            public async Task<IActionResult> OnGetAsync(int id)
            {
                
                var response = await _flowerService.GetByIdAsync(id);

              
                if (!response.IsSuccess)
                {
                    return NotFound(response.Message);
                }

               
                Flower = response.Model;

                return Page(); 
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

