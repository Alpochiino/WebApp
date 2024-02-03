using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    [Authorize(Roles = "User")]
    public class DeleteOrderModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public DeleteOrderModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var order = await context.Orders.FindAsync(Order.OrderId);

                if (order == null)
                {
                    return NotFound();
                }

                var car = await context.Cars.FindAsync(order.CarId);

                if (car != null)
                {
                    car.IsAvailable = true;
                    car.Status = CarStatus.Available;
                }

                context.Orders.Remove(order);
                await context.SaveChangesAsync();

                TempData["success"] = "Order canceled successfully";
                return RedirectToPage("./IndexUser");
            }
            catch
            {
                TempData["error"] = "Warning: Order couldn't be canceled. Try again later.";
                return Page();
            }
        }
    }
}
