using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Username field is required.")]
        public required string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} characters", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters (1 Uppercase) (1 Number) (1 Special Character)")]
        public required string Password { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        public required string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter Last Name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public required string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})", ErrorMessage = "Entered phone format is not valid.")]
        public required string PhoneNumber { get; set; }

        public string Role { get; set; } = "User";
    }
}
