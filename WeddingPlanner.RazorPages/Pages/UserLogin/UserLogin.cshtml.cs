using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.DTO;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;

namespace WeddingPlanner.RazorPages.Pages.UserLogin
{
    public class UserLoginModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly SessionManager _sessionManager;
        public UserLoginModel(AuthService authService, SessionManager sessionManager)
        {
            _authService = authService;
            _sessionManager = sessionManager;
        }


        [BindProperty]
        public LoginModelDTO User { get; set; }
        [BindProperty]
        public LoginResponse LoginResponse { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {


            LoginResponse = await _authService.LoginUser(User);
            _sessionManager.SetToken(LoginResponse.Token);
            return RedirectToPage("/Index");
        }
    }
}

