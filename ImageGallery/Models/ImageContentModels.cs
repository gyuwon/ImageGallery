using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ImageGallery.Models
{
    public abstract class ImageContentModelBase
    {
        [Required, DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }

    public class ImageContentBindingModel : ImageContentModelBase
    {
    }

    public abstract class ImageContentEntityBase : ImageContentModelBase
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class ImageContent : ImageContentEntityBase
    {
        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ImageContentComment> Comments { get; set; }
    }

    public class ImageContentViewModel : ImageContentEntityBase
    {
        public string UserName { get; set; }
        public List<ImageContentCommentViewModel> Comments { get; set; }
    }
}
