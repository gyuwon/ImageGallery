using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGallery.Models
{
    public abstract class ImageContentModelBase
    {
        [Key]
        public int Id { get; set; }
        [Required, DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }

    public class ImageContent : ImageContentModelBase
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class ImageContentViewModel : ImageContentModelBase
    {
        public string UserName { get; set; }
    }
}
