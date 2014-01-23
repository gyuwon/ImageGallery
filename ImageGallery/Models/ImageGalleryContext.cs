using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImageGallery.Models
{
    public class ImageGalleryContext : IdentityDbContext<User>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ImageGalleryContext()
            : base("name=DefaultConnection")
        {
            this.Database.Log += message => System.Diagnostics.Debug.WriteLine(message);
        }

        public DbSet<ImageContent> ImageContents { get; set; }
        public DbSet<ImageContentComment> ImageContentComments { get; set; }
    }
}
