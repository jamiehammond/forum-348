using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public String Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
