using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Interfaces;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ICar carRep;
        private readonly ApplicationDbContext context;

        public IndexModel(ICar carRep, ApplicationDbContext context)
        {
            this.carRep = carRep;
        }

        public IEnumerable<Car> Cars { get; set; }

        public void OnGet()
        {
            Cars = carRep.GetAllCars();
        }
    }
}
