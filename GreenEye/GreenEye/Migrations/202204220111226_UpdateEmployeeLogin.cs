namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Username", c => c.String());
            AddColumn("dbo.Employees", "Password", c => c.String());
            AddColumn("dbo.Employees", "Entropy", c => c.String());
            AddColumn("dbo.Employees", "Remember", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Remember");
            DropColumn("dbo.Employees", "Entropy");
            DropColumn("dbo.Employees", "Password");
            DropColumn("dbo.Employees", "Username");
        }
    }
}
