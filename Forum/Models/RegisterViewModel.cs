using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(256, MinimumLength = 4)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}