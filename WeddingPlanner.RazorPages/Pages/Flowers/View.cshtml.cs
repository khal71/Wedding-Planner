using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using System.Threading.Tasks;
namespace WeddingPlanner.RazorPages.Pages.Flowers
{
    
        public class ViewFlowerModel : PageModel
        {
            private readonly FlowerService _flowerService;

            
            public ViewFlowerModel(FlowerService flowerService)
            {
                _flowerService = flowerService;
            }

           
            public Flower Flower { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var flower = await _flowerService.GetFlowerByIdAsync(id);


            if (flower.Id == null)
                {
                   
                    return NotFound();
                }

            Flower = flower;

                return Page(); 
            }
        }
    }

