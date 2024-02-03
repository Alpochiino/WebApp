using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly ICar carRep;

        public DetailsModel(ICar carRep)
        {
            this.carRep = carRep;
        }

        public Car Car { get; set; }

        public IActionResult OnGet(int id)
        {
            Car = carRep.GetCarById(id);

            if (Car == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
