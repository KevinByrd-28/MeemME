namespace MeemME.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Folder", "Images_DataGroupField", c => c.String());
            AddColumn("dbo.Folder", "Images_DataTextField", c => c.String());
            AddColumn("dbo.Folder", "Images_DataValueField", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Folder", "Images_DataValueField");
            DropColumn("dbo.Folder", "Images_DataTextField");
            DropColumn("dbo.Folder", "Images_DataGroupField");
        }
    }
}
