using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly IUser userRep;

        public LoginModel(IUser userRep)
        {
            this.userRep = userRep;
        }

        [BindProperty]
        public new User User { get; set; }

        public IActionResult OnGet()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var users = await userRep.GetUserByEmailAsync(User.Email);

            try
            {
                if (users.Any())
                {
                    foreach (var user in users)
                    {
                        if (user.Password == this.User.Password)
                        {
                            List<Claim> claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name, user.FirstName),
                                new Claim(ClaimTypes.NameIdentifier, this.User.Email),
                                new Claim(ClaimTypes.Role, user.Role),
                            };

                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                CookieAuthenticationDefaults.AuthenticationScheme);

                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                                IsPersistent = true,
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                            TempData["success"] = "Logged In";
                            return RedirectToPage("/Rents/Index");
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
