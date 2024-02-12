using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly IUser userRep;

        public DetailsModel(IUser userRep)
        {
            this.userRep = userRep;
        }

        public User UserDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDetail = await userRep.GetUserByIdAsync(id.Value);

            if (UserDetail == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
