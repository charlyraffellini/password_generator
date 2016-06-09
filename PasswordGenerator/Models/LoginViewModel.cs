using System.ComponentModel.DataAnnotations;

namespace PasswordGenerator.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email or UserID ")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}