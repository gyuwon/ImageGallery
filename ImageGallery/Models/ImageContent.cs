using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImageGallery.Models
{
    public class ImageContent
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required, DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
