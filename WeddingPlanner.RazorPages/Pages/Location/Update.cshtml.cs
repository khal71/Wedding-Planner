using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Location
{
    public class UpdateLocationModel : PageModel
    {
        private readonly LocationService _locationService;

        public UpdateLocationModel(LocationService locationService)
        {
            _locationService = locationService;
        }
        [BindProperty] 
        public WeddingPlannerDomain.Entities.Location Location { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Location = await _locationService.GetLocationByIdAsnc(id); 
            if (Location == null)
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
                    ModelState.AddModelError("ImageFile", "Please upload a valid image file (JPEG, PNG, GIF).");
                    return Page();
                }
                using (var memeoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memeoryStream);
                    Location.ImageData = memeoryStream.ToArray();
                }
            }
            await _locationService.UpdateLocationAsync(Location);
            return RedirectToPage("Index"); 
        }
    }
}
