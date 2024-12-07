using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.DTO;
using WeddingPlanner.RazorPages.Pages.Auth;

namespace WeddingPlanner.RazorPages.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly SessionManager _sessionManager;
        public CreateModel(AuthService authService, SessionManager sessionManager)
        {
            _authService = authService;
            _sessionManager = sessionManager;
        }


        [BindProperty]
        public LoginModelDTO User { get; set; }
     

        public async Task<IActionResult> OnPostAsync()
        {

           await _authService.RegisterUser(User);
            return Page();
        }
    }
}

