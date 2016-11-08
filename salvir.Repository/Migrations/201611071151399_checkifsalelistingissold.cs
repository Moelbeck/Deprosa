namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkifsalelistingissold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleListings", "IsSold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleListings", "IsSold");
        }
    }
}
