using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Models.User
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Email must not be empty.")]
        [EmailAddress(ErrorMessage = "Email must be valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password must not be empty.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 chars long.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Password must contain at least one number, one lowercase letter, one uppercase letter and one special char.")]
        public string Password { get; set; }
    }
}
