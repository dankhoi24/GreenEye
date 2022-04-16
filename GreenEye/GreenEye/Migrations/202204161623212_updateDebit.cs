namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDebit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DebitBooks", "DebitBookId", "dbo.Customers");
            DropIndex("dbo.DebitBooks", new[] { "DebitBookId" });
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.DebitBooks", "DebitBookId", c => c.Int(nullable: false, identity: true));
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.DebitBooks", "DebitBookId");
            CreateIndex("dbo.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.DebitBooks");
            AlterColumn("dbo.DebitBooks", "DebitBookId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "CustomerId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.DebitBooks", "DebitBookId");
            AddForeignKey("dbo.DebitBooks", "DebitBookId", "dbo.Customers", "CustomerId");
        }
    }
}
