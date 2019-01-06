using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class CreatePostViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
    }
}
