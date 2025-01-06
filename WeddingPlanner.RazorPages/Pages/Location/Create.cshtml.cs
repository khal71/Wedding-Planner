using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;


namespace WeddingPlanner.RazorPages.Pages.Location
{
    public class CreateLocationModel : PageModel
    {
        private readonly LocationService _locationService;
        private readonly SessionManager _sessionManager; 
        public CreateLocationModel(LocationService locationService, SessionManager sessionManager)
        {
            _locationService = locationService;
        }
        [BindProperty]
        public WeddingPlannerDomain.Entities.Location Location { get; set; }
        [BindProperty]
        public bool isAdmin { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }    

        public void OnGet()
        {
            isAdmin = _sessionManager.IsAdmin;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Location.Id = 0; 
            if (ImageFile != null)
            {
                var allowedExtensions = new[] { "image/jpeg", "image/png", "image/gif" }; 
                if (!allowedExtensions.Contains(ImageFile.ContentType))
                {
                    ModelState.AddModelError("ImageFile", "Please upload a valid image file (JPEG, PNG, GIF).");
                    return Page(); 
                }

                using (var memeryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memeryStream);
                    Location.ImageData = memeryStream.ToArray();
                }
            }

            var res = await _locationService.AddLocationAsync(Location);

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
