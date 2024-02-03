using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Admins
{
    public class LoginModel : PageModel
    {
        private readonly IAdmin adminRep;

        [BindProperty]
        public Admin Model { get; set; }

        public LoginModel(IAdmin adminRep)
        {
            this.adminRep = adminRep;
        }

        public IActionResult OnGet()
        {
            ClaimsPrincipal claimAdmin = HttpContext.User;

            if (claimAdmin.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Cars/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var admins = adminRep.GetAdminByEmail(Model.Email);

            try
            {
                if (admins.Any())
                {
                    foreach (var admin in admins)
                    {
                        if (admin.Password == Model.Password)
                        {
                            List<Claim> claims = new List<Claim>()
                            {
                            new Claim(ClaimTypes.Name, admin.Username),
                            new Claim(ClaimTypes.NameIdentifier, Model.Email),
                            new Claim(ClaimTypes.Role, admin.Role)
                            };

                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);

                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                                IsPersistent = Model.ToString() == "true",
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                            TempData["success"] = "Logged In as Admin";
                            return RedirectToPage("/Cars/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid email or password");
                            return Page();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }

                TempData["error"] = "Log In Failed";
                return Page();
            }
            catch
            {
                TempData["error"] = "Log In Failed";
                return Page();
            }
        }
    }
}
