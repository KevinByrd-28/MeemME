namespace MeemME.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Folder", "Images_DataGroupField");
            DropColumn("dbo.Folder", "Images_DataTextField");
            DropColumn("dbo.Folder", "Images_DataValueField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Folder", "Images_DataValueField", c => c.String());
            AddColumn("dbo.Folder", "Images_DataTextField", c => c.String());
            AddColumn("dbo.Folder", "Images_DataGroupField", c => c.String());
        }
    }
}
