using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IUser userRep;

        public CreateModel(IUser userRep)
        {
            this.userRep = userRep;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User Model { get; set; }

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


                        await userRep.AddUserAsync(newUser);
                        await userRep.SaveChangesAsync();

                        TempData["success"] = "Account created successfully";
                        return RedirectToPage("/Users/Index");
                    }
                    else
                    {
                        if (existingUsers.Any())
                        {
                            ModelState.AddModelError("User.Username", "Användarnamnet är redan upptaget");
                        }

                        if (await userRep.IsPhoneNumberTakenAsync(Model.PhoneNumber))
                        {
                            ModelState.AddModelError("User.PhoneNumber", "Telefonnumret är redan upptaget");
                        }

                        if (await userRep.IsEmailTakenAsync(Model.Email))
                        {
                            ModelState.AddModelError("User.Email", "E-postadressen är redan upptagen");
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
