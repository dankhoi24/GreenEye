namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookTypes",
                c => new
                    {
                        BookTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BookTypeId);
            
            AddColumn("dbo.Books", "BookTypeId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Books", "BookTypeId", "dbo.BookTypes", "BookTypeId", cascadeDelete: true);
            CreateIndex("dbo.Books", "BookTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Books", new[] { "BookTypeId" });
            DropForeignKey("dbo.Books", "BookTypeId", "dbo.BookTypes");
            DropColumn("dbo.Books", "BookTypeId");
            DropTable("dbo.BookTypes");
        }
    }
}
