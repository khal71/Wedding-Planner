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
        public Flower Flower { get; set; }
        [BindProperty]
        public bool isAdmin  { get; set; }


        public void OnGet()
        {
             isAdmin = _sessionManager.IsAdmin;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _flowerService.AddFlowerAsync(Flower);

            return RedirectToPage("Index");
        }
    }
}

