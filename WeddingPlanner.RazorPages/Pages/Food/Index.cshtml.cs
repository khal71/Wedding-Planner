using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerApplication.Services.ServicesInterfaces;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;


namespace WeddingPlanner.RazorPages.Pages.Food
{
    public class IndexFoodModel : PageModel
    {
        private readonly FoodService _foodService;
        private readonly SessionManager _sessionManager;

        public IndexFoodModel(FoodService foodService, SessionManager sessionManager)
        {
            _foodService = foodService;
            _sessionManager = sessionManager;
        }
        public List<WeddingPlannerDomain.Entities.Food> Foods { get; set; }

        [BindProperty]
        public bool isloggedin { get; set; }
        [BindProperty]
        public bool isAdmin {  get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            isAdmin = _sessionManager.IsAdmin;
            isloggedin = _sessionManager.IsAuthenticated; 
            if (isloggedin == true)
            {
                Foods = await _foodService.GetAllFoodsAsync();
                if (Foods !=null)
                {
                    return Page(); 
                }
                else { return Page(); }

            }
            else
            {
                return RedirectToPage("/UserLogin/UserLogin"); 
            }
        }
      
    }
}
