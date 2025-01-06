using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Location
{
    public class DeleteLocationModel : PageModel
    {
        private readonly LocationService _locationService;

        public DeleteLocationModel(LocationService locationService)
        {
            _locationService = locationService;
        }
        [BindProperty]
        public WeddingPlannerDomain.Entities.Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Location = await _locationService.GetLocationByIdAsnc(id);
            if (Location == null)
            {
                return NotFound();
            }
            return Page(); 
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var res = await _locationService.DeleteLocationAsync(id);
            if (res == true)
            {
                return RedirectToPage("/Index");

            }
            else
            {
                ModelState.AddModelError("", "Failed to delete the location");
                return Page(); 
            }
        }
    }
}
