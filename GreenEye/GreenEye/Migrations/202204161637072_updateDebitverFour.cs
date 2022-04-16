namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDebitverFour : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.DebitBooks");
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.DebitBooks", "DebitBookId", c => c.Int(nullable: false));
            AddForeignKey("dbo.DebitBooks", "DebitBookId", "dbo.Customers", "CustomerId");
            CreateIndex("dbo.DebitBooks", "DebitBookId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DebitBooks", new[] { "DebitBookId" });
            DropForeignKey("dbo.DebitBooks", "DebitBookId", "dbo.Customers");
            AlterColumn("dbo.DebitBooks", "DebitBookId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.DebitBooks", "DebitBookId");
        }
    }
}
