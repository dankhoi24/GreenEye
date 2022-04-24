namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Date");
        }
    }
}
