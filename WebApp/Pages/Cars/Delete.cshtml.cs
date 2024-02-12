using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ICar carRep;

        public DeleteModel(ICar carRep)
        {
            this.carRep = carRep;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Car = await carRep.GetCarByIdAsync(id);
            if (Car == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Car.CarId > 0) 
            {
                await carRep.DeleteCarAsync(Car.CarId);
                TempData["success"] = "Car deleted successfully";
            }
            else
            {
                TempData["error"] = "Warning: Car couldn't be deleted. Try again later.";
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
