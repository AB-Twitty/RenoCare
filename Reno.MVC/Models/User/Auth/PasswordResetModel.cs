using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Reno.MVC.Models.User.Auth
{
    public class PasswordResetModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [DisplayName("Confirmation Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
        public string Otp { get; set; }
    }
}
