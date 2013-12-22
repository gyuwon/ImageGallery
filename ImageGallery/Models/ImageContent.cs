using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Models
{
    public class ImageContent
    {
        [Key]
        public int ContentId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
