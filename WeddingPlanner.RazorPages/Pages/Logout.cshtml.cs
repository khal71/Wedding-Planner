using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;

namespace WeddingPlanner.RazorPages.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SessionManager _sessionManager;
        public LogoutModel(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;

        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
             _sessionManager.ClearToken();
            return RedirectToPage("/Index");
        }
    }
}
