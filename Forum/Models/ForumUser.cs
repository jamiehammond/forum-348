using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class ForumUser : IdentityUser
    {
        // Inherits Id, username, etc. from IdentityUser, so not specified here
    }
}
