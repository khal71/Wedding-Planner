using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Flowers
{
    public class IndexFlowerModel : PageModel
    {

        private readonly FlowerService _flowerService;
        private readonly SessionManager _sessionManager;

        public IndexFlowerModel(FlowerService flowerService, SessionManager sessionManager)
        {
            _flowerService = flowerService;
            _sessionManager = sessionManager;
        }

        public List<Flower> Flowers { get; set; }

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
            Flowers = await _flowerService.GetAllFlowersAsync();
                if (Flowers != null)
                {
                    return Page();
                }
                else { return Page(); }
            }
           else
            {// change to user login
              return RedirectToPage("/UserLogin/UserLogin");
            }
        }
    }
}

