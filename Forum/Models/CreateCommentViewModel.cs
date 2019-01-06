using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
