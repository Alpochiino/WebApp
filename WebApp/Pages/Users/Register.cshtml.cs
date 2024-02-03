using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly IUser userRep;

        [BindProperty]
        public User Model { get; set; }

        public RegisterModel(IUser userRep)
        {
            this.userRep = userRep;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Rents/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var existingUsers = userRep.GetUserByUsername(Model.Username);
                    if (!existingUsers.Any() && !userRep.IsPhoneNumberTaken(Model.PhoneNumber) && !userRep.IsEmailTaken(Model.Email))
                    {
                        var newUser = new User
                        {
                            FirstName = Model.FirstName,
                            LastName = Model.LastName,
                            Email = Model.Email,
                            PhoneNumber = Model.PhoneNumber,
                            Password = Model.Password,
                            Username = Model.Username,
                            Role = "User",
                        };

                        newUser.Role = "User";

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Model.Username),
                        new Claim(ClaimTypes.NameIdentifier, Model.Email),
                        new Claim(ClaimTypes.Role, "User")
                    };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            IsPersistent = Model.ToString() == "true",
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                        userRep.AddUser(newUser);
                        userRep.SaveChanges();
                        TempData["success"] = "Account created successfully";
                        return RedirectToPage("/Rents/Index");
                    }
                    else
                    {
                        if (existingUsers.Any())
                        {
                            ModelState.AddModelError("Model.Username", "Användarnamnet är redan upptaget");
                        }

                        if (userRep.IsPhoneNumberTaken(Model.PhoneNumber))
                        {
                            ModelState.AddModelError("Model.PhoneNumber", "Telefonnumret är redan upptaget");
                        }

                        if (userRep.IsEmailTaken(Model.Email))
                        {
                            ModelState.AddModelError("Model.Email", "E-postadressen är redan upptagen");
                        }
                    }
                }
                return Page();
            }
            catch
            {
                TempData["error"] = "Warning: User couldn't be created. Try again later.";
                return Page();
            }
        }
    }
}
