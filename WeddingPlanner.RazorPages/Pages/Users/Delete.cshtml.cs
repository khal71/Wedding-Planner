using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly UserServiceR _userService;

        public DeleteModel(UserServiceR userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _userService.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var res = await _userService.DeleteUserAsync(id);

            if (res == true)
            {

                return RedirectToPage("Index"); // Redirect after deletion
            }
            else
            {
                // Handle failure
                ModelState.AddModelError("", "Failed to delete the flower.");
                return Page();
            }

        }
    }
}
