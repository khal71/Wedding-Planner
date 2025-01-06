using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Location
{
    public class IndexLocationModel : PageModel
    {
        private readonly LocationService _locationService;
        private readonly SessionManager _sessionManager;

        public IndexLocationModel(LocationService locationService, SessionManager sessionManager)
        {
            _locationService = locationService;
            _sessionManager = sessionManager;
        }

        public List<WeddingPlannerDomain.Entities.Location> Locations { get; set; }

        [BindProperty]
        public bool isLoggedin { get; set; }

        [BindProperty]
        public bool isAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            isAdmin = _sessionManager.IsAdmin;
            isLoggedin = _sessionManager.IsAuthenticated;
            if (isLoggedin == true)
            {
                Locations = await _locationService.GetAllLocationAsync();
                if (Locations != null)
                {
                    return Page();
                }
                else { return Page(); }
            }
            else
            {
                return RedirectToPage("UserLogin/UserLogin"); 
            }
        }

    }
}
