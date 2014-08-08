namespace PaniniWS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAlbumColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAlbumStickers", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.UserAlbumStickers", new[] { "Album_AlbumID" });
            DropColumn("dbo.UserAlbumStickers", "Album_AlbumID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAlbumStickers", "Album_AlbumID", c => c.Int());
            CreateIndex("dbo.UserAlbumStickers", "Album_AlbumID");
            AddForeignKey("dbo.UserAlbumStickers", "Album_AlbumID", "dbo.Albums", "AlbumID");
        }
    }
}
