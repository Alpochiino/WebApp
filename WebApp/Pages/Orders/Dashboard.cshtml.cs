using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    public class DashboardModel : PageModel
    {
        private readonly IUser userRep;
        private readonly ApplicationDbContext context;

        public DashboardModel(IUser userRep, ApplicationDbContext context)
        {
            this.userRep = userRep;
            this.context = context;
        }

        public List<Order> Orders { get; set; }

        public void OnGet()
        {
            var currentUser = userRep.GetUserByUsername(User.Identity.Name).FirstOrDefault();

            if (currentUser != null)
            {
                Orders = context.Orders
                    .Where(o => o.UserId == currentUser.Id)
                    .OrderByDescending(o => o.StartDate)
                    .ToList();
            }
        }
    }
}
