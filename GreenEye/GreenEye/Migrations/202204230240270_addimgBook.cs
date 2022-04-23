namespace GreenEye.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimgBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Img", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Img");
        }
    }
}
