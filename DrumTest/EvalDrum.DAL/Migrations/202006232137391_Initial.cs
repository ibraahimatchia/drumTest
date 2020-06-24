namespace EvalDrum.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrumManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContactEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrumNumber = c.String(),
                        CreatedOn = c.DateTime(),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        InPositionSince = c.DateTime(),
                        Site_Id = c.Int(),
                        LastStatusUpdate = c.DateTime(),
                        Status_Id = c.Int(),
                        DrumManager_Id = c.Int(),
                        DrumManager_Id1 = c.Int(),
                        Site_Id1 = c.Int(),
                        Status_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DrumManagers", t => t.DrumManager_Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.DrumManager_Id)
                .Index(t => t.Site_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        Street2 = c.String(),
                        Street3 = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Number = c.String(),
                        Tel = c.String(),
                        Fax = c.String(),
                        EMail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status_name = c.String(),
                        CreatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drums", "Status_Id1", "dbo.Status");
            DropForeignKey("dbo.Drums", "Site_Id1", "dbo.Sites");
            DropForeignKey("dbo.Drums", "DrumManager_Id1", "dbo.DrumManagers");
            DropIndex("dbo.Drums", new[] { "Status_Id1" });
            DropIndex("dbo.Drums", new[] { "Site_Id1" });
            DropIndex("dbo.Drums", new[] { "DrumManager_Id1" });
            DropTable("dbo.Status");
            DropTable("dbo.Sites");
            DropTable("dbo.Drums");
            DropTable("dbo.DrumManagers");
        }
    }
}
