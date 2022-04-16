namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GoodsReceiptManagements", "BookId", "dbo.Books");
            DropForeignKey("dbo.GoodsReceiptManagements", "GoodsReceiptId", "dbo.GoodsReceipts");
            DropIndex("dbo.GoodsReceiptManagements", new[] { "BookId" });
            DropIndex("dbo.GoodsReceiptManagements", new[] { "GoodsReceiptId" });
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
            
            AddColumn("dbo.Books", "Publisher", c => c.String());
            AddColumn("dbo.Books", "ImportPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "ExportPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "Sales", c => c.Int(nullable: false));
            AddColumn("dbo.GoodsReceipts", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Books", "DatePublished");
            DropColumn("dbo.Books", "PriceImport");
            DropColumn("dbo.Books", "Price");
            DropColumn("dbo.Books", "Sale");
            DropColumn("dbo.GoodsReceipts", "DateImport");
            DropTable("dbo.GoodsReceiptManagements");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GoodsReceiptManagements",
                c => new
                    {
                        GoodsManagementId = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookId = c.Int(nullable: false),
                        GoodsReceiptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GoodsManagementId);
            
            AddColumn("dbo.GoodsReceipts", "DateImport", c => c.DateTime(nullable: false));
            AddColumn("dbo.Books", "Sale", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "PriceImport", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Books", "DatePublished", c => c.DateTime(nullable: false));
            DropIndex("dbo.GoodsReceipt_Book", new[] { "GoodsReceiptId" });
            DropIndex("dbo.GoodsReceipt_Book", new[] { "BookId" });
            DropForeignKey("dbo.GoodsReceipt_Book", "GoodsReceiptId", "dbo.GoodsReceipts");
            DropForeignKey("dbo.GoodsReceipt_Book", "BookId", "dbo.Books");
            DropColumn("dbo.GoodsReceipts", "Date");
            DropColumn("dbo.Books", "Sales");
            DropColumn("dbo.Books", "ExportPrice");
            DropColumn("dbo.Books", "ImportPrice");
            DropColumn("dbo.Books", "Publisher");
            DropTable("dbo.GoodsReceipt_Book");
            CreateIndex("dbo.GoodsReceiptManagements", "GoodsReceiptId");
            CreateIndex("dbo.GoodsReceiptManagements", "BookId");
            AddForeignKey("dbo.GoodsReceiptManagements", "GoodsReceiptId", "dbo.GoodsReceipts", "GoodsReceiptId", cascadeDelete: true);
            AddForeignKey("dbo.GoodsReceiptManagements", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
        }
    }
}
