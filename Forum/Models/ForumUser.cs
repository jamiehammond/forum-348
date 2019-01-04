using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ForumUser : IdentityUser
    {

        [Required]
        [StringLength(256, MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        public bool IsAdmin { get; set; }
    }
}
