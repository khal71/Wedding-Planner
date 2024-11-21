using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
    
        public class ViewFlowerModel : PageModel
        {
            private readonly IFlowerService _flowerService;

            
            public ViewFlowerModel(IFlowerService flowerService)
            {
                _flowerService = flowerService;
            }

           
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
        }
    }

