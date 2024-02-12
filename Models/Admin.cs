using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Admin
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        public string Username { get; set; } = "Admin";

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password")]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} characters", MinimumLength = 8)]
        [RegularExpression(@"^([a-zA-Z0-9@*#]{8,15})$", ErrorMessage = "Password must contain: Minimum 8 characters, at least 1 UpperCase Alphabet, 1 Number, and a 1 Special Character")]
        public string Password { get; set; }

        public string Role { get; set; } = "Admin";
    }
}
