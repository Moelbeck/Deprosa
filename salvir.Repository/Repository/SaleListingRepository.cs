using System;
using System.Collections.Generic;
using System.Linq;
using deprosa.Model;
using deprosa.Repository.DatabaseContext;
using deprosa.Repository.Abstract;
using deprosa.Interfaces;

namespace deprosa.Repository
{
    public class SaleListingRepository : GenericRepository<SaleListing>, ISaleListingRepository
    {
        public SaleListingRepository(BzaleDatabaseContext context) : base(context)
        {

        }
        public SaleListing AddSaleListing(SaleListing newSaleListing)
        {
            Add(newSaleListing);
            Save();
            return newSaleListing;
        }

        public SaleListing UpdateSaleListing(SaleListing updatedSaleListing)
        {
            Edit(updatedSaleListing);
            Save();
            return GetSingle(e => e.ID == updatedSaleListing.ID);
        }

        public SaleListing GetSaleListing(int itemid)
        {
            return GetSingle(e => e.ID == itemid && e.Deleted == null && e.ExpirationDate > DateTime.Now);
        }

        public void DeleteSaleListing(SaleListing SaleListing)
        {
            Delete(SaleListing);
            Save();
        }
        public void DeleteSaleListing(int id)
        {
            Delete(id);
            Save();
        }
        public IQueryable<SaleListing> GetSaleListingsForCompany(string vatnumber)
        {
            return Get(e => e.Owner.VAT == vatnumber && e.Deleted == null);
        }
        public IQueryable<SaleListing> GetSaleListingsForCompany(int id)
        {
            return Get(e => e.Owner.ID == id && e.Deleted == null);
        }
        public IQueryable<SaleListing> GetSaleListingsForCategory(int id)
        {
            return Get(e => e.ProductType.Category.ID == id);
        }
        public IQueryable<SaleListing> GetDeletedSaleListingsForCompany(string vatnumber)
        {
            return Get(e => e.Owner.VAT == vatnumber && e.Deleted != null);
        }

        public void AddNewImageForSaleListing(int SaleListingid,Image newimage)
        {
            var SaleListing = GetSaleListing(SaleListingid);
            
                SaleListing.Images.Add(newimage);
                Edit(SaleListing);
                Save();
        }

        public void RemoveImageForSaleListing(int SaleListingid, Image newimage)
        {
            var SaleListing = GetSaleListing(SaleListingid);

                SaleListing.Images.Remove(newimage);
                Edit(SaleListing);
                Save();
        }

        public void UpdateSaleListingSubscription(SaleListing salelisting, Subscription sub)
        {
            var sale = GetSaleListing(salelisting.ID); //To be sure that we have the correct sale.
            sale.Subscription = sub;
            Edit(sale);
            Save();
        }

        public IQueryable<SaleListing> GetAllSaleListings()
        {
            return Get(e => e.Deleted == null);
        }
    }
}
