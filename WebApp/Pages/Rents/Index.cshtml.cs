using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Rents
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ICar carRep;
        private readonly ApplicationDbContext context;

        public IndexModel(ICar carRep, ApplicationDbContext context)
        {
            this.carRep = carRep;
            this.context = context;
        }

        public IEnumerable<Car> Cars { get; set; }

        public async Task OnGetAsync()
        {
            UpdateCarStatus();
            Cars = await carRep.GetAllAvailableCarsAsync();
        }

        public void UpdateCarStatus()
        {
            var currentDate = DateTime.Now;
            var activeOrders = context.Orders
                .Where(o => o.EndDate >= currentDate)
                .OrderByDescending(o => o.EndDate)
                .ToList();

            var cars = context.Cars.ToList();

            foreach (var car in cars)
            {
                var latestActiveOrder = activeOrders.FirstOrDefault(o => o.CarId == car.CarId);

                car.Status = latestActiveOrder != null ? CarStatus.Rented : CarStatus.Available;
                car.IsAvailable = latestActiveOrder != null ? car.IsAvailable : car.IsAvailable = true;
            }

            context.SaveChanges();
        }
    }
}
