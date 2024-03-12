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
                    var existingUsers = await userRep.GetUserByUsernameAsync(Model.Username);
                    bool isPhoneNumberTaken = await userRep.IsPhoneNumberTakenAsync(Model.PhoneNumber);
                    bool isEmailTaken = await userRep.IsEmailTakenAsync(Model.Email);
                    
                    if (!existingUsers.Any() && !isPhoneNumberTaken && !isEmailTaken)
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
                            IsPersistent = true,
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        await userRep.AddUserAsync(newUser);
                        await userRep.SaveChangesAsync();

                        TempData["success"] = "Account created successfully";
                        return RedirectToPage("/Rents/Index");
                    }
                    else
                    {
                        if (existingUsers.Any())
                        {
                            ModelState.AddModelError("Model.Username", "Användarnamnet är redan upptaget");
                        }

                        if (await userRep.IsPhoneNumberTakenAsync(Model.PhoneNumber))
                        {
                            ModelState.AddModelError("Model.PhoneNumber", "Telefonnumret är redan upptaget");
                        }

                        if (await userRep.IsEmailTakenAsync(Model.Email))
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
