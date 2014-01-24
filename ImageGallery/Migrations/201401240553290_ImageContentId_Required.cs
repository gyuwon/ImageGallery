namespace ImageGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageContentId_Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageContentComments", "CommentText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageContentComments", "CommentText", c => c.String());
        }
    }
}
