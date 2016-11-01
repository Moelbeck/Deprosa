namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conditionOnSalelistings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SaleListings", "Condition", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleListings", "Condition");
        }
    }
}
