namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Books", "Publisher", c => c.String(maxLength: 200));
            AlterColumn("dbo.BookTypes", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Customers", "Email", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 150));
            AlterColumn("dbo.Employees", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Employees", "Phone", c => c.String(maxLength: 15));
            AlterColumn("dbo.Employees", "Address", c => c.String(maxLength: 150));
            AlterColumn("dbo.Employees", "Role", c => c.String(maxLength: 50));
            AlterColumn("dbo.Promotions", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Promotions", "Name", c => c.String());
            AlterColumn("dbo.Employees", "Role", c => c.String());
            AlterColumn("dbo.Employees", "Address", c => c.String());
            AlterColumn("dbo.Employees", "Phone", c => c.String());
            AlterColumn("dbo.Employees", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.BookTypes", "Name", c => c.String());
            AlterColumn("dbo.Books", "Publisher", c => c.String());
            AlterColumn("dbo.Books", "Name", c => c.String());
        }
    }
}
