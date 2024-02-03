using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUser userRep;

        public DetailsModel(IUser userRep)
        {
            this.userRep = userRep;
        }

        public User UserDetail { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDetail = userRep.GetUserById(id.Value);

            if (UserDetail == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
