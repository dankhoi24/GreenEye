namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConnectBook_Order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order_Book",
                c => new
                    {
                        Order_BookId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_BookId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order_Book", new[] { "OrderId" });
            DropIndex("dbo.Order_Book", new[] { "BookId" });
            DropForeignKey("dbo.Order_Book", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Order_Book", "BookId", "dbo.Books");
            DropTable("dbo.Order_Book");
        }
    }
}
