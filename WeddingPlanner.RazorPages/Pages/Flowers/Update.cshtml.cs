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
       
        [BindProperty]
        public IFormFile ImageFile { get; set; }

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
           
            if (ImageFile != null)
            {
                var allowedExtensions = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedExtensions.Contains(ImageFile.ContentType))
                {
                    // Handle invalid file type
                    ModelState.AddModelError("ImageFile", "Please upload a valid image file (JPEG, PNG, GIF).");
                    return Page();
                }

                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    Flower.ImageData = memoryStream.ToArray();
                }
            }
           

            await _flowerService.UpdateFlowerAsync(Flower);

            return RedirectToPage("Index");
        }
    }


        }
    

