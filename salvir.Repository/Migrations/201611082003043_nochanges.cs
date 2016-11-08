namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nochanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LogCategories", "MainCategoryId");
            AddColumn("dbo.LogCategories", "MainCategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogCategories", "MainCategoryId");
            AddColumn("dbo.LogCategories", "MainCategoryId", c => c.Int(nullable: false));
        }
    }
}
