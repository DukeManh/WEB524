namespace Assignment5_SanghyukLee.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMediaItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaItems",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                        StringId = c.String(nullable: false),
                        Caption = c.String(nullable: false),
                        Content = c.Binary(),
                        ContentType = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Artist_ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MediaId)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistId)
                .Index(t => t.Artist_ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MediaItems", "Artist_ArtistId", "dbo.Artists");
            DropIndex("dbo.MediaItems", new[] { "Artist_ArtistId" });
            DropTable("dbo.MediaItems");
        }
    }
}
