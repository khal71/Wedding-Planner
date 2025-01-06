using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{

    public class CreateFlowerModel : PageModel
    {
        private readonly FlowerService _flowerService;
        private readonly SessionManager _sessionManager;

        public CreateFlowerModel(FlowerService flowerService, SessionManager sessionManager)
        {
            _flowerService = flowerService;
            _sessionManager = sessionManager;
        }

        [BindProperty]
        public WeddingPlannerDomain.Flower Flower { get; set; }
        [BindProperty]
        public bool isAdmin  { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public void OnGet()
        {
             isAdmin = _sessionManager.IsAdmin;
        }
        

        public async Task<IActionResult> OnPostAsync()
        {
            Flower.Id = 0;

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


            var res = await _flowerService.AddFlowerAsync(Flower);

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

