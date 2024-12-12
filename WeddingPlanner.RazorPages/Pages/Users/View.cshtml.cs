using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeddingPlannerApplication.Services.ServicesImplementation;
using WeddingPlannerDomain;
using WeddingPlannerDomain.Entities;

namespace WeddingPlanner.RazorPages.Pages.Users
{
    public class ViewModel : PageModel
    {
        private readonly UserServiceR _userService;


        public ViewModel(UserServiceR userService)
        {
            _userService = userService;
        }


        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);


            if (user.Id == null)
            {

                return NotFound();
            }

            User = user;

            return Page();
        }
    }
}
