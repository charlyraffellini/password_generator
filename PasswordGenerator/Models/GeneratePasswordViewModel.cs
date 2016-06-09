using System.ComponentModel.DataAnnotations;

namespace PasswordGenerator.Models
{
    public class GeneratePasswordViewModel
    {
        [Required]
        [Display(Name = "Email or UserID")]
        [EmailAddress]
        public string Email { get; set; }
    }
}