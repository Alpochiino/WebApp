using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    [Authorize(Roles = "User")]
    public class ConfirmationModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IUser userRep;

        public ConfirmationModel(IUser userRep, ApplicationDbContext context)
        {
            this.userRep = userRep;
            this.context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public async Task OnGetAsync()
        {
            var currentUser = (await userRep.GetUserByUsernameAsync(User.Identity.Name)).FirstOrDefault();

            try
            {
                if (currentUser != null)
                {
                    Order = context.Orders.Include(o => o.Car).FirstOrDefault(o => o.UserId == currentUser.Id && o.OrderId == OrderId);
                }
                else
                {
                    RedirectToPage("./Dashboard");
                }
            }
            catch
            {
                Page();
            }
        }
    }
}
