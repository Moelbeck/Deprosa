using deprosa.Model;

namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoggingToDB2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BaseComments", newName: "Comments");
            CreateTable(
                "dbo.LogCategories",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    UserID = c.Int(nullable: false),
                    MainCategoryId = c.Int(nullable:true),
                    SubCategoryId = c.Int(nullable:true),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                    Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                })
                .PrimaryKey(t => t.ID);
            CreateTable(
                "dbo.LogLogins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogSaleListings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SaleListingID = c.Int(nullable: false),
                        MainCategoryId = c.Int(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        LogType = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogSearches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SearchString = c.String(),
                        UserID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
        }
        
        public override void Down()
        {
            DropTable("dbo.LogSearches");
            DropTable("dbo.LogSaleListings");
            DropTable("dbo.LogLogins");
            DropTable("dbo.LogCategories");
            RenameTable(name: "dbo.Comments", newName: "BaseComments");
        }
    }
}
