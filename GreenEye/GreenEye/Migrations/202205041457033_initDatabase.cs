namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 50),
                        Address = c.String(maxLength: 150),
                        Coin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.DebitBooks",
                c => new
                    {
                        DebitBookId = c.Int(nullable: false, identity: true),
                        BeginDebit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDebit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DebitBookId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        PromotionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Promotions", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 15),
                        Address = c.String(maxLength: 150),
                        Role = c.String(maxLength: 50),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Username = c.String(),
                        Password = c.String(),
                        Entropy = c.String(),
                        Remember = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.GoodsReceipts",
                c => new
                    {
                        GoodsReceiptId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GoodsReceiptId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.GoodsReceipt_Book",
                c => new
                    {
                        GoodsReceipt_BookId = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        GoodsReceiptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GoodsReceipt_BookId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.GoodsReceipts", t => t.GoodsReceiptId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.GoodsReceiptId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Publisher = c.String(maxLength: 200),
                        Author = c.String(maxLength: 200),
                        Date = c.DateTime(nullable: false),
                        Img = c.String(maxLength: 200),
                        ImportPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExportPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stroke = c.Int(nullable: false),
                        Sales = c.Int(nullable: false),
                        BookTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.BookTypes", t => t.BookTypeId, cascadeDelete: true)
                .Index(t => t.BookTypeId);
            
            CreateTable(
                "dbo.BookTypes",
                c => new
                    {
                        BookTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.BookTypeId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
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
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentDiscount = c.Int(nullable: false),
                        MaxDiscount = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId);
            
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
            DropForeignKey("dbo.Refunds", "RefundId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.Orders", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.GoodsReceipt_Book", "GoodsReceiptId", "dbo.GoodsReceipts");
            DropForeignKey("dbo.Order_Book", "BookId", "dbo.Books");
            DropForeignKey("dbo.Order_Book", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Inventories", "BookId", "dbo.Books");
            DropForeignKey("dbo.GoodsReceipt_Book", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "BookTypeId", "dbo.BookTypes");
            DropForeignKey("dbo.GoodsReceipts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DebitBooks", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Bills", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Refunds", new[] { "RefundId" });
            DropIndex("dbo.Order_Book", new[] { "OrderId" });
            DropIndex("dbo.Order_Book", new[] { "BookId" });
            DropIndex("dbo.Inventories", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "BookTypeId" });
            DropIndex("dbo.GoodsReceipt_Book", new[] { "GoodsReceiptId" });
            DropIndex("dbo.GoodsReceipt_Book", new[] { "BookId" });
            DropIndex("dbo.GoodsReceipts", new[] { "EmployeeId" });
            DropIndex("dbo.Orders", new[] { "PromotionId" });
            DropIndex("dbo.Orders", new[] { "EmployeeId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.DebitBooks", new[] { "CustomerId" });
            DropIndex("dbo.Bills", new[] { "CustomerId" });
            DropTable("dbo.Refunds");
            DropTable("dbo.Promotions");
            DropTable("dbo.Order_Book");
            DropTable("dbo.Inventories");
            DropTable("dbo.BookTypes");
            DropTable("dbo.Books");
            DropTable("dbo.GoodsReceipt_Book");
            DropTable("dbo.GoodsReceipts");
            DropTable("dbo.Employees");
            DropTable("dbo.Orders");
            DropTable("dbo.DebitBooks");
            DropTable("dbo.Customers");
            DropTable("dbo.Bills");
        }
    }
}
