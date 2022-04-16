namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectOrderToEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Role = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            AddColumn("dbo.Orders", "EmployeeId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            CreateIndex("dbo.Orders", "EmployeeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropColumn("dbo.Orders", "EmployeeId");
            DropTable("dbo.Employees");
        }
    }
}
