using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly ICar carRep;
        private readonly IWebHostEnvironment webHost;

        public EditModel(ICar carRep, IWebHostEnvironment webHost)
        {
            this.carRep = carRep;
            this.webHost = webHost;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task <IActionResult> OnGetAsync(int id)
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
            if (ModelState.IsValid)
            {
                if (Car.ImageFiles != null && Car.ImageFiles.Count > 0)
                {
                    Car.ImagePath = new List<string>();

                    foreach (var imageFile in Car.ImageFiles)
                    {
                        if (imageFile != null && imageFile.Length > 0)
                        {
                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                            var imagePath = Path.Combine(webHost.WebRootPath, "images", "Car1", uniqueFileName);

                            using (var stream = new FileStream(imagePath, FileMode.Create))
                            {
                                imageFile.CopyTo(stream);
                            }

                            Car.ImagePath.Add($"/images/Car1/{uniqueFileName}");
                        }
                    }
                }

                await carRep.UpdateCarAsync(Car);
                TempData["success"] = "Car edited successfully";
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["error"] = "Warning: Car couldn't be edited. Try again later.";
                return Page();
            }
        }
    }
}
