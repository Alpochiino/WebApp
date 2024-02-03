using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;

        [BindProperty]
        public Order Order { get; set; }

        public DeleteModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0 || context.Orders == null)
            {
                return NotFound();
            }

            Order = await context.Orders.FindAsync(id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Order == null || Order.OrderId == 0)
            {
                return NotFound();
            }

            Order = await context.Orders.FindAsync(Order.OrderId);

            if (Order == null)
            {
                return NotFound();
            }

            try
            {
                var car = await context.Cars.FindAsync(Order.CarId);

                if (car != null)
                {
                    car.IsAvailable = true;
                    car.Status = CarStatus.Available;
                }

                context.Orders.Remove(Order);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToPage("./Index");
            }

            TempData["success"] = "Order canceled successfully";
            return RedirectToPage("./Index");
        }
    }

}
