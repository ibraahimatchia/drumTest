namespace EvalDrum.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_ForeignKey : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Drums", "DrumManager_Id");
            DropColumn("dbo.Drums", "Site_Id");
            DropColumn("dbo.Drums", "Status_Id");
            RenameColumn(table: "dbo.Drums", name: "DrumManager_Id1", newName: "DrumManager_Id");
            RenameColumn(table: "dbo.Drums", name: "Site_Id1", newName: "Site_Id");
            RenameColumn(table: "dbo.Drums", name: "Status_Id1", newName: "Status_Id");
            RenameIndex(table: "dbo.Drums", name: "IX_Site_Id1", newName: "IX_Site_Id");
            RenameIndex(table: "dbo.Drums", name: "IX_Status_Id1", newName: "IX_Status_Id");
            RenameIndex(table: "dbo.Drums", name: "IX_DrumManager_Id1", newName: "IX_DrumManager_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Drums", name: "IX_DrumManager_Id", newName: "IX_DrumManager_Id1");
            RenameIndex(table: "dbo.Drums", name: "IX_Status_Id", newName: "IX_Status_Id1");
            RenameIndex(table: "dbo.Drums", name: "IX_Site_Id", newName: "IX_Site_Id1");
            RenameColumn(table: "dbo.Drums", name: "Status_Id", newName: "Status_Id1");
            RenameColumn(table: "dbo.Drums", name: "Site_Id", newName: "Site_Id1");
            RenameColumn(table: "dbo.Drums", name: "DrumManager_Id", newName: "DrumManager_Id1");
            AddColumn("dbo.Drums", "Status_Id", c => c.Int());
            AddColumn("dbo.Drums", "Site_Id", c => c.Int());
            AddColumn("dbo.Drums", "DrumManager_Id", c => c.Int());
        }
    }
}
