using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUser userRep;

        public IndexModel(IUser userRep)
        {
            this.userRep = userRep;
        }

        public IEnumerable<User> Users { get; set; }

        public IActionResult OnGet()
        {
            Users = userRep.GetAllUser();
            return Page();
        }
    }
}
