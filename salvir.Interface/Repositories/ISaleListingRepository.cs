using deprosa.Model;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.Interfaces
{
    public interface ISaleListingRepository
    {
       SaleListing AddSaleListing(SaleListing newSaleListing);

       SaleListing UpdateSaleListing(SaleListing updatedSaleListing);

       SaleListing GetSaleListing(int itemid);

       void DeleteSaleListing(SaleListing SaleListing);
       void DeleteSaleListing(int id);
       IQueryable<SaleListing> GetSaleListingsForCompany(string vatnumber);
        IQueryable<SaleListing> GetSaleListingsForCompany(int id);
        IQueryable<SaleListing> GetSaleListingsForCategory(int id);
        IQueryable<SaleListing> GetDeletedSaleListingsForCompany(string vatnumber);
        IQueryable<SaleListing> GetAllSaleListings();

       void AddNewImageForSaleListing(int SaleListingid, Image newimage);
       void RemoveImageForSaleListing(int SaleListingid, Image newimage);

       void UpdateSaleListingSubscription(SaleListing salelisting, Subscription sub);
    }          
}
