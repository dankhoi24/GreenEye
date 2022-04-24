namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IdBook = c.Int(nullable: false),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "Book_BookId", "dbo.Books");
            DropIndex("dbo.Inventories", new[] { "Book_BookId" });
            DropTable("dbo.Inventories");
        }
    }
}
