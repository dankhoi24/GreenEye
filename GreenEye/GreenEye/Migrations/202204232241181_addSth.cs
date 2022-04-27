namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AmountInOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "AmountInOrder");
        }
    }
}
