using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
