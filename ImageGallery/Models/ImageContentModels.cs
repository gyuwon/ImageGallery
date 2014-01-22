using System;
using System.Collections.Generic;
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
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<ImageContentComment> Comments { get; set; }
    }

    public class ImageContentViewModel : ImageContentModelBase
    {
        [Required]
        public string UserName { get; set; }
    }
}
