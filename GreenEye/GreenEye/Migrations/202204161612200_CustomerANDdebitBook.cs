namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerANDdebitBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.DebitBooks",
                c => new
                    {
                        DebitBookId = c.Int(nullable: false),
                        BeginDebit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDebit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DebitBookId)
                .ForeignKey("dbo.Customers", t => t.DebitBookId)
                .Index(t => t.DebitBookId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DebitBooks", new[] { "DebitBookId" });
            DropForeignKey("dbo.DebitBooks", "DebitBookId", "dbo.Customers");
            DropTable("dbo.DebitBooks");
            DropTable("dbo.Customers");
        }
    }
}
