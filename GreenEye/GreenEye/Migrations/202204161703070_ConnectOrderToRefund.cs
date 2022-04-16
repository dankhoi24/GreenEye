namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectOrderToRefund : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        RefundId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RefundId)
                .ForeignKey("dbo.Orders", t => t.RefundId)
                .Index(t => t.RefundId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Refunds", new[] { "RefundId" });
            DropForeignKey("dbo.Refunds", "RefundId", "dbo.Orders");
            DropTable("dbo.Refunds");
        }
    }
}
