using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

        public async Task OnGetAsync()
        {
            var currentUser = (await userRep.GetUserByUsernameAsync(User.Identity.Name)).FirstOrDefault();


            if (currentUser != null)
            {
                Orders = await context.Orders
                    .Where(o => o.UserId == currentUser.Id)
                    .OrderByDescending(o => o.StartDate)
                    .ToListAsync();
            }
        }
    }
}
