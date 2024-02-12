using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IOrder orderRep;

        public IndexModel(IOrder orderRep)
        {
            this.orderRep = orderRep;
        }

        public IEnumerable<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await orderRep.GetAllOrdersAsync();
        }
    }
}
