using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Comment
    {

        public int Id { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        [StringLength(750, MinimumLength = 1)]
        public string Content { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        public Post Post { get; set; }
    }
}
