namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBill : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Customers", "Coin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.Bills", new[] { "CustomerId" });
            DropForeignKey("dbo.Bills", "CustomerId", "dbo.Customers");
            DropColumn("dbo.Customers", "Coin");
            DropTable("dbo.Bills");
        }
    }
}
