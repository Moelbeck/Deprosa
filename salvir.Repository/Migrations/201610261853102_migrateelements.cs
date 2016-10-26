namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrateelements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        HasValidatedMail = c.Boolean(nullable: false),
                        Password = c.String(),
                        Salt = c.String(),
                        CompanyID = c.Int(),
                        CountryCode = c.Int(nullable: false),
                        City = c.String(),
                        PostalCode = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VAT = c.String(),
                        CountryCode = c.Int(nullable: false),
                        City = c.String(),
                        PostalCode = c.Int(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        Image_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.Image_ID)
                .Index(t => t.Image_ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                        ImageData = c.Binary(),
                        Type = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        SaleListing_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SaleListings", t => t.SaleListing_ID)
                .Index(t => t.SaleListing_ID);
            
            CreateTable(
                "dbo.SaleListings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Height = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Depth = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Thickness = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        CPU = c.String(),
                        RAM = c.Int(nullable: false),
                        ScreenSize = c.Double(nullable: false),
                        Harddisk = c.Int(nullable: false),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        Kilometers = c.Int(nullable: false),
                        FuelType = c.String(),
                        KmPrLiter = c.Double(nullable: false),
                        Color = c.String(),
                        LastService = c.String(),
                        NoOfDoors = c.Int(nullable: false),
                        VatPayed = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy_ID = c.Int(),
                        Manufacturer_ID = c.Int(),
                        Owner_ID = c.Int(),
                        ProductType_ID = c.Int(),
                        Subscription_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_ID)
                .ForeignKey("dbo.Companies", t => t.Owner_ID)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_ID)
                .ForeignKey("dbo.Subscriptions", t => t.Subscription_ID)
                .Index(t => t.CreatedBy_ID)
                .Index(t => t.Manufacturer_ID)
                .Index(t => t.Owner_ID)
                .Index(t => t.ProductType_ID)
                .Index(t => t.Subscription_ID);
            
            CreateTable(
                "dbo.BaseComments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Message = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsPrivateMessage = c.Boolean(),
                        CommentType = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ParentComment_ID = c.Int(),
                        SaleListing_ID = c.Int(),
                        Sender_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BaseComments", t => t.ParentComment_ID)
                .ForeignKey("dbo.SaleListings", t => t.SaleListing_ID)
                .ForeignKey("dbo.Accounts", t => t.Sender_ID)
                .Index(t => t.ParentComment_ID)
                .Index(t => t.SaleListing_ID)
                .Index(t => t.Sender_ID);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Types = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        Category_ID = c.Int(),
                        Manufacturer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SubCategories", t => t.Category_ID)
                .ForeignKey("dbo.Manufacturers", t => t.Manufacturer_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.Manufacturer_ID);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        MainCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MainCategories", t => t.MainCategory_ID)
                .Index(t => t.MainCategory_ID);
            
            CreateTable(
                "dbo.MainCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        Image_ID = c.Int(),
                        CategoryPreferences_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.Image_ID)
                .ForeignKey("dbo.CategoryPreferences", t => t.CategoryPreferences_ID)
                .Index(t => t.Image_ID)
                .Index(t => t.CategoryPreferences_ID);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        SubscriptionCategory = c.Int(nullable: false),
                        SubscriptionType = c.Int(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Advertisements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        URL = c.String(),
                        ExpirationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        Advertiser_ID = c.Int(),
                        Image_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Advertisers", t => t.Advertiser_ID)
                .ForeignKey("dbo.Images", t => t.Image_ID)
                .Index(t => t.Advertiser_ID)
                .Index(t => t.Image_ID);
            
            CreateTable(
                "dbo.Advertisers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        URL = c.String(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CategoryPreferences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        Account_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.Account_ID)
                .Index(t => t.Account_ID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        GivenRating = c.Int(nullable: false),
                        Votes = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Updated = c.DateTime(precision: 7, storeType: "datetime2"),
                        Deleted = c.DateTime(precision: 7, storeType: "datetime2"),
                        Account_ID = c.Int(),
                        Company_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.Account_ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .Index(t => t.Account_ID)
                .Index(t => t.Company_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.Ratings", "Account_ID", "dbo.Accounts");
            DropForeignKey("dbo.BaseComments", "Sender_ID", "dbo.Accounts");
            DropForeignKey("dbo.MainCategories", "CategoryPreferences_ID", "dbo.CategoryPreferences");
            DropForeignKey("dbo.CategoryPreferences", "Account_ID", "dbo.Accounts");
            DropForeignKey("dbo.Advertisements", "Image_ID", "dbo.Images");
            DropForeignKey("dbo.Advertisements", "Advertiser_ID", "dbo.Advertisers");
            DropForeignKey("dbo.Accounts", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.SaleListings", "Subscription_ID", "dbo.Subscriptions");
            DropForeignKey("dbo.SaleListings", "ProductType_ID", "dbo.ProductTypes");
            DropForeignKey("dbo.SaleListings", "Owner_ID", "dbo.Companies");
            DropForeignKey("dbo.SaleListings", "Manufacturer_ID", "dbo.Manufacturers");
            DropForeignKey("dbo.ProductTypes", "Manufacturer_ID", "dbo.Manufacturers");
            DropForeignKey("dbo.ProductTypes", "Category_ID", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "MainCategory_ID", "dbo.MainCategories");
            DropForeignKey("dbo.MainCategories", "Image_ID", "dbo.Images");
            DropForeignKey("dbo.Images", "SaleListing_ID", "dbo.SaleListings");
            DropForeignKey("dbo.SaleListings", "CreatedBy_ID", "dbo.Accounts");
            DropForeignKey("dbo.BaseComments", "SaleListing_ID", "dbo.SaleListings");
            DropForeignKey("dbo.BaseComments", "ParentComment_ID", "dbo.BaseComments");
            DropForeignKey("dbo.Companies", "Image_ID", "dbo.Images");
            DropIndex("dbo.Ratings", new[] { "Company_ID" });
            DropIndex("dbo.Ratings", new[] { "Account_ID" });
            DropIndex("dbo.CategoryPreferences", new[] { "Account_ID" });
            DropIndex("dbo.Advertisements", new[] { "Image_ID" });
            DropIndex("dbo.Advertisements", new[] { "Advertiser_ID" });
            DropIndex("dbo.MainCategories", new[] { "CategoryPreferences_ID" });
            DropIndex("dbo.MainCategories", new[] { "Image_ID" });
            DropIndex("dbo.SubCategories", new[] { "MainCategory_ID" });
            DropIndex("dbo.ProductTypes", new[] { "Manufacturer_ID" });
            DropIndex("dbo.ProductTypes", new[] { "Category_ID" });
            DropIndex("dbo.BaseComments", new[] { "Sender_ID" });
            DropIndex("dbo.BaseComments", new[] { "SaleListing_ID" });
            DropIndex("dbo.BaseComments", new[] { "ParentComment_ID" });
            DropIndex("dbo.SaleListings", new[] { "Subscription_ID" });
            DropIndex("dbo.SaleListings", new[] { "ProductType_ID" });
            DropIndex("dbo.SaleListings", new[] { "Owner_ID" });
            DropIndex("dbo.SaleListings", new[] { "Manufacturer_ID" });
            DropIndex("dbo.SaleListings", new[] { "CreatedBy_ID" });
            DropIndex("dbo.Images", new[] { "SaleListing_ID" });
            DropIndex("dbo.Companies", new[] { "Image_ID" });
            DropIndex("dbo.Accounts", new[] { "CompanyID" });
            DropTable("dbo.Ratings");
            DropTable("dbo.CategoryPreferences");
            DropTable("dbo.Advertisers");
            DropTable("dbo.Advertisements");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.MainCategories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.BaseComments");
            DropTable("dbo.SaleListings");
            DropTable("dbo.Images");
            DropTable("dbo.Companies");
            DropTable("dbo.Accounts");
        }
    }
}
