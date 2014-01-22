using System;
using System.ComponentModel.DataAnnotations;

namespace ImageGallery.Models
{
    public abstract class ImageContentCommentBase
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int ImageContentId { get; set; }

        public virtual ImageContent ImageContent { get; set; }
    }

    public class ImageContentComment : ImageContentCommentBase
    {
        public string UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public virtual User User { get; set; }
    }

    public class ImageContentCommentViewModel : ImageContentCommentBase
    {
        [Required]
        public string UserName { get; set; }
    }
}