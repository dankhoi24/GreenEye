namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateinventory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inventories", "Book_BookId", "dbo.Books");
            DropIndex("dbo.Inventories", new[] { "Book_BookId" });
            RenameColumn(table: "dbo.Inventories", name: "Book_BookId", newName: "BookId");
            AlterColumn("dbo.Inventories", "BookId", c => c.Int(nullable: false));
            CreateIndex("dbo.Inventories", "BookId");
            AddForeignKey("dbo.Inventories", "BookId", "dbo.Books", "BookId", cascadeDelete: true);
            DropColumn("dbo.Inventories", "IdBook");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "IdBook", c => c.Int(nullable: false));
            DropForeignKey("dbo.Inventories", "BookId", "dbo.Books");
            DropIndex("dbo.Inventories", new[] { "BookId" });
            AlterColumn("dbo.Inventories", "BookId", c => c.Int());
            RenameColumn(table: "dbo.Inventories", name: "BookId", newName: "Book_BookId");
            CreateIndex("dbo.Inventories", "Book_BookId");
            AddForeignKey("dbo.Inventories", "Book_BookId", "dbo.Books", "BookId");
        }
    }
}
