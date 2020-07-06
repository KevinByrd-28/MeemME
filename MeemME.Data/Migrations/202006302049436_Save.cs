namespace MeemME.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Save : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Save",
                c => new
                    {
                        SaveID = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.SaveID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Save");
        }
    }
}
