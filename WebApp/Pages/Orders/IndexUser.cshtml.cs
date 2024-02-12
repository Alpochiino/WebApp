using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Interfaces;
using WebApplic.Models;

namespace WebApp.Pages.Orders
{
    [Authorize(Roles = "User")]
    public class IndexUserModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IUser userRep;

        public IndexUserModel(ApplicationDbContext context, IUser userRep)
        {
            this.context = context;
            this.userRep = userRep;
        }

        public OrderViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = User.Identity.Name;
            var currentUser = (await userRep.GetUserByUsernameAsync(username)).FirstOrDefault();

            if (currentUser != null)
            {
                var currentOrders = await context.Orders
                    .Where(o => o.UserId == currentUser.Id && o.EndDate >= DateTime.Now)
                    .ToListAsync();

                var previousOrders = await context.Orders
                    .Where(o => o.UserId == currentUser.Id && o.EndDate < DateTime.Now)
                    .ToListAsync();

                ViewModel = new OrderViewModel
                {
                    CurrentOrders = currentOrders,
                    PreviousOrders = previousOrders
                };

                return Page();
            }

            TempData["error"] = $"User with username '{username}' not found.";
            return RedirectToPage("/Index");
        }
    }
}
