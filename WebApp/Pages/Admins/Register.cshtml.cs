using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Admins
{
    public class RegisterModel : PageModel
    {
        private readonly IAdmin adminRep;

        [BindProperty]
        public Admin Model { get; set; }

        public RegisterModel(IAdmin adminRep)
        {
            this.adminRep = adminRep;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToPage("/Cars/Index");
                }

                return RedirectToPage("/Rents/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var existingAdmins = await adminRep.GetAdminByEmailAsync(Model.Email);
                if (!existingAdmins.Any())
                {
                    var newAdmin = new Admin
                    {
                        Email = Model.Email,
                        Password = Model.Password,
                        Username = "Admin",
                        Role = "Admin",
                    };

                    newAdmin.Role = "Admin";

                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, newAdmin.Username),
                        new Claim(ClaimTypes.NameIdentifier, newAdmin.Email),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = Model.ToString() == "true",
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                    
                    await adminRep.AddAdminAsync(newAdmin);
                    await adminRep.SaveChangesAsync();

                    TempData["success"] = "Admin created successfully";
                    return RedirectToPage("/Cars/Index");
                }
                else
                {
                    if (existingAdmins.Any())
                    {
                        ModelState.AddModelError("Model.Email", "E-postadressen är redan upptagen");
                    }
                }
            }
            return Page();
        }
    }
}
