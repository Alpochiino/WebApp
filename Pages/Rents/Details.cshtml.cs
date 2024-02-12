using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Rents
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ICar carRep;

        public DetailsModel(ICar carRep)
        {
            this.carRep = carRep;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Car = await carRep.GetCarByIdAsync(Id);

            if (Car == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
