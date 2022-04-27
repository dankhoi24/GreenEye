namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmployeeReceipt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoodsReceipts", "EmployeeId", c => c.Int(nullable: true));
            CreateIndex("dbo.GoodsReceipts", "EmployeeId");
            AddForeignKey("dbo.GoodsReceipts", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoodsReceipts", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.GoodsReceipts", new[] { "EmployeeId" });
            DropColumn("dbo.GoodsReceipts", "EmployeeId");
        }
    }
}
