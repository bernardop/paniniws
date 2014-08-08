namespace PaniniWS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumPages",
                c => new
                    {
                        AlbumPageID = c.Int(nullable: false, identity: true),
                        PageNumber = c.Int(nullable: false),
                        Description = c.String(),
                        PageImageFilePath = c.String(),
                        Album_AlbumID = c.Int(),
                    })
                .PrimaryKey(t => t.AlbumPageID)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AlbumCoverFilePath = c.String(),
                    })
                .PrimaryKey(t => t.AlbumID);
            
            CreateTable(
                "dbo.AlbumStickers",
                c => new
                    {
                        AlbumStickerID = c.Int(nullable: false, identity: true),
                        StickerNumber = c.Int(nullable: false),
                        Description = c.String(),
                        StickerImageFilePath = c.String(),
                        AlbumPage_AlbumPageID = c.Int(),
                    })
                .PrimaryKey(t => t.AlbumStickerID)
                .ForeignKey("dbo.AlbumPages", t => t.AlbumPage_AlbumPageID)
                .Index(t => t.AlbumPage_AlbumPageID);
            
            CreateTable(
                "dbo.UserAlbumStickers",
                c => new
                    {
                        UserAlbumStickerID = c.Int(nullable: false, identity: true),
                        Have = c.Boolean(nullable: false),
                        HaveRepeated = c.Boolean(nullable: false),
                        AlbumSticker_AlbumStickerID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserAlbumStickerID)
                .ForeignKey("dbo.AlbumStickers", t => t.AlbumSticker_AlbumStickerID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.AlbumSticker_AlbumStickerID)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAlbumStickers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAlbumStickers", "AlbumSticker_AlbumStickerID", "dbo.AlbumStickers");
            DropForeignKey("dbo.AlbumStickers", "AlbumPage_AlbumPageID", "dbo.AlbumPages");
            DropForeignKey("dbo.AlbumPages", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.UserAlbumStickers", new[] { "User_Id" });
            DropIndex("dbo.UserAlbumStickers", new[] { "AlbumSticker_AlbumStickerID" });
            DropIndex("dbo.AlbumStickers", new[] { "AlbumPage_AlbumPageID" });
            DropIndex("dbo.AlbumPages", new[] { "Album_AlbumID" });
            DropTable("dbo.UserAlbumStickers");
            DropTable("dbo.AlbumStickers");
            DropTable("dbo.Albums");
            DropTable("dbo.AlbumPages");
        }
    }
}
