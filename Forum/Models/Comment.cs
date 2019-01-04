using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        [StringLength(750, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }
    }
}
