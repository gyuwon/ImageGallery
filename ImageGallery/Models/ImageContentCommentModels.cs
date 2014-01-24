using System;
using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Models
{
    public abstract class ImageContentCommentBase
    {
        [Required]
        public string CommentText { get; set; }
    }

    public class ImageContentCommentBindingModel : ImageContentCommentBase
    {
    }

    public abstract class ImageContentCommandEntityBase : ImageContentCommentBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ImageContentId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class ImageContentComment : ImageContentCommandEntityBase
    {
        public string UserId { get; set; }

        public virtual ImageContent ImageContent { get; set; }
        public virtual User User { get; set; }
    }

    public class ImageContentCommentViewModel : ImageContentCommandEntityBase
    {
        public string UserName { get; set; }
    }
}