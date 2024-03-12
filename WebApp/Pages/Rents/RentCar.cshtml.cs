using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Rents
{
    [Authorize(Roles = "User")]
    public class RentCarModel : PageModel
    {
        private readonly ICar carRep;
        private readonly IUser userRep;
        private readonly ApplicationDbContext context;

        public RentCarModel(ICar carRep, IUser userRep, ApplicationDbContext context)
        {
            this.carRep = carRep;
            this.userRep = userRep;
            this.context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public decimal TotalPrice { get; set; }

        [BindProperty]
        public decimal Price { get; set; }

        public Car Car { get; set; }

        public async Task OnGetAsync()
        {
            Car = await carRep.GetCarByIdAsync(Id);

            if (Car == null || Car.Status != CarStatus.Available)
            {
                RedirectToPage("/Rents/Index");
            }

            Price = Car.Price; 
            TotalPrice = Price;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Car = await carRep.GetCarByIdAsync(Id);

            if (Car == null || Car.Status != CarStatus.Available)
            {
                TempData["error"] = "This car is not available for rent.";
                return RedirectToPage("/Rents/Index");
            }

            if (StartDate >= EndDate)
            {
                TempData["error"] = "Invalid date range. Please select a valid date range.";
                return RedirectToPage("/Rents/RentCar", new { id = Id });
            }

            Price = Car.Price;

            int numberOfDays = (int)(EndDate - StartDate).TotalDays;
            decimal increasePrice = 500; //(If you want to change the price you need to do the same in RentCar.cshtml!!!!)
            decimal totalPrice = Price + increasePrice * numberOfDays;

            TotalPrice = totalPrice;

            //(Uncomment the selected lines if you want the car to not display in the rent page when it's rented)
            //Car.IsAvailable = false;
            Car.Status = CarStatus.Rented;
            await context.SaveChangesAsync();

            var currentUserTask = userRep.GetUserByUsernameAsync(User.Identity.Name);
            var users = await currentUserTask;
            var currentUser = users.FirstOrDefault();

            if (currentUser != null)
            {
                try
                {
                    var order = new Order
                    {
                        CarId = Car.CarId,
                        UserId = currentUser.Id,
                        StartDate = StartDate,
                        EndDate = EndDate,
                        TotalPrice = totalPrice,
                    };

                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();

                    int orderId = order.OrderId;

                    TempData["success"] = "Car rented successfully!";
                    return RedirectToPage("/Orders/Confirmation", new { OrderId = orderId });
                }
                catch
                {
                    TempData["error"] = "Warning: Car couldn't be rented. Try again later.";
                    return RedirectToPage("/Rents/Index");
                }
            }
            else
            {
                TempData["error"] = "User not found.";
                return RedirectToPage("/Rents/Index");
            }
        }
    }
}
