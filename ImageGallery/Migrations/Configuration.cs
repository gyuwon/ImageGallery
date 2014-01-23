namespace ImageGallery.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ImageGallery.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ImageGallery.Models.ImageGalleryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ImageGallery.Models.ImageGalleryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            User user = new User
            {
                Id = "ebc2c948-bac3-4cd4-9e1b-a08121d0ec98",
                UserName = "gyuwon",
                PasswordHash = "AIBcwtGNCZJiH9owKu6znsFRdMBZ/HvCBGxPX2LUnXhSSLpZZ/WQeREnxaDwKbcTwg==",
                SecurityStamp = "c9031ce9-0d78-4020-8106-623e735ff847"
            };

            context.Users.Add(user);

            DateTime now = DateTime.Now.ToUniversalTime();
            for (int i = 100; i > 0; i--)
            {
                context.ImageContents.Add(new ImageContent
                {
                    UserId = user.Id,
                    ImageUrl = "https://cmfhhw.bn1303.livefilestore.com/y2mGsMqpGe1NUcwvcRhIFW1MYLVlBIBPtOfirX-50S3JemOMFS40N3ggAXfYb6R0Tn3Vy7TTjR7AuxreMKEisTlPD3M4qxm0KfK5yQIaeCMvka05WV60wSJ-lem954RUYwk/IMG_1964.JPG?psid=1",
                    Description = "ÀÌ¶×ÀÌ",
                    Created = now.AddDays(-i),
                    Updated = now.AddDays(-i)
                });
            }
        }
    }
}
