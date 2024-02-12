using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ICar carRep;
        private readonly IWebHostEnvironment webHost;

        public CreateModel(ICar carRep, IWebHostEnvironment webHost)
        {
            this.carRep = carRep;
            this.webHost = webHost;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Reached here: beginning [Action/Step]");
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

                await carRep.AddCarAsync(Car);
                TempData["success"] = "Car created successfully";
                return RedirectToPage("./Index");
            }
            else
            {
                Console.WriteLine("It's not valid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return Page();
            }
        }
    }
}
