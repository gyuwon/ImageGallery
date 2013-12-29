using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImageGallery.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<ImageContent> ImageContents { get; set; }
    }
}