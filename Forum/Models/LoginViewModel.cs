using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class LoginViewModel
    {

        [Required]
        public String Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}
