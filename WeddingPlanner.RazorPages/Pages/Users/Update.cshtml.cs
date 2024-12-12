using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlanner.DTO;
using WeddingPlanner.RazorPages.Pages.Auth;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Users
{
    public class UpdateModel : PageModel
    {
        private readonly UserServiceR _userService;
        private readonly AuthService _authService;
        public UpdateModel(UserServiceR userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [BindProperty]
        public User User { get; set; }

        [BindProperty]
        public LoginModelDTO UserL { get; set; }
        [BindProperty]
        public String newPassword { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _userService.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var hashpass = _authService.HashPassword(newPassword);
                User.Password = hashpass;
            }
            else
            {
                var existingUser = await _userService.GetUserByIdAsync(User.Id);
                if (existingUser != null)
                {
                    User.Password = existingUser.Password;
                }
            }

            await _userService.UpdateUserAsync(User);

            return RedirectToPage("Index");
        }
    }
}
