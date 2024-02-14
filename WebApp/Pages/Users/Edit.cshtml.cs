using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public EditModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public new User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || context.Users == null)
            {
                return NotFound();
            }

            User = await context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Id != User.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(User);
                    await context.SaveChangesAsync();
                }
                catch
                {
                    return Page();
                }
            }

            return RedirectToPage("Index");
        }
    }
}
