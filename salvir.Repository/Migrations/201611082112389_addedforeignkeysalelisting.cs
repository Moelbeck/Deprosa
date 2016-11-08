namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforeignkeysalelisting : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SaleListings", name: "Owner_ID", newName: "CompanyId");
            RenameColumn(table: "dbo.SaleListings", name: "CreatedBy_ID", newName: "AccountId");
            RenameColumn(table: "dbo.SaleListings", name: "Manufacturer_ID", newName: "ManufacturerId");
            RenameColumn(table: "dbo.SaleListings", name: "ProductType_ID", newName: "ProductTypeId");
            RenameIndex(table: "dbo.SaleListings", name: "IX_Owner_ID", newName: "IX_CompanyId");
            RenameIndex(table: "dbo.SaleListings", name: "IX_CreatedBy_ID", newName: "IX_AccountId");
            RenameIndex(table: "dbo.SaleListings", name: "IX_Manufacturer_ID", newName: "IX_ManufacturerId");
            RenameIndex(table: "dbo.SaleListings", name: "IX_ProductType_ID", newName: "IX_ProductTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.SaleListings", name: "IX_ProductTypeId", newName: "IX_ProductType_ID");
            RenameIndex(table: "dbo.SaleListings", name: "IX_ManufacturerId", newName: "IX_Manufacturer_ID");
            RenameIndex(table: "dbo.SaleListings", name: "IX_AccountId", newName: "IX_CreatedBy_ID");
            RenameIndex(table: "dbo.SaleListings", name: "IX_CompanyId", newName: "IX_Owner_ID");
            RenameColumn(table: "dbo.SaleListings", name: "ProductTypeId", newName: "ProductType_ID");
            RenameColumn(table: "dbo.SaleListings", name: "ManufacturerId", newName: "Manufacturer_ID");
            RenameColumn(table: "dbo.SaleListings", name: "AccountId", newName: "CreatedBy_ID");
            RenameColumn(table: "dbo.SaleListings", name: "CompanyId", newName: "Owner_ID");
        }
    }
}
