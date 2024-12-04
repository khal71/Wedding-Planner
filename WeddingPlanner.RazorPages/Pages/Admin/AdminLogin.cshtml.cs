using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.DTO;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly SessionManager _sessionManager;
        public AdminLoginModel(AuthService authService, SessionManager sessionManager)
        {
            _authService = authService;
            _sessionManager = sessionManager;
        }
      

        [BindProperty]
        public LoginModelDTO Admin { get; set; }
        [BindProperty]
        public LoginResponse LoginResponse { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {


            LoginResponse = await _authService.LoginAdmin(Admin);
            _sessionManager.SetToken(LoginResponse.Token);
            return Page();
        }
    }
}

