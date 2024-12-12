using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Users
{
    public class IndexModel : PageModel
    {

        private readonly UserServiceR _userService;
        private readonly SessionManager _sessionManager;

        public IndexModel(UserServiceR userService, SessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        public List<User> Users { get; set; }

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
                Users = await _userService.GetAllUsersAsync();
                if (Users != null)
                {
                    return Page();
                }
                else { return Page(); }
            }
            else
            {// change to admin login
                return RedirectToPage("/Admin/AdminLogin");
            }
        }
    }
}
