namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectOrderToPromotion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentDiscount = c.Int(nullable: false),
                        MaxDiscount = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId);
            
            AddColumn("dbo.Orders", "PromotionId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Orders", "PromotionId", "dbo.Promotions", "PromotionId", cascadeDelete: true);
            CreateIndex("dbo.Orders", "PromotionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "PromotionId" });
            DropForeignKey("dbo.Orders", "PromotionId", "dbo.Promotions");
            DropColumn("dbo.Orders", "PromotionId");
            DropTable("dbo.Promotions");
        }
    }
}
