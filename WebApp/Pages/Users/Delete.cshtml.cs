using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public DeleteModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public new User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                User = await context.Users.FindAsync(id);

                if (User != null)
                {
                    try
                    {
                        context.Users.Remove(User);
                        await context.SaveChangesAsync();
                    }
                    catch
                    {
                        return Page();
                    }

                    TempData["success"] = "Customer Deleted successfully.";
                    return RedirectToPage("Index");
                }

            }
            catch
            {
                return Page();
            }

            return Page();
        }
    }
}
