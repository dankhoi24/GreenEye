namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authorBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Author", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Author");
        }
    }
}
