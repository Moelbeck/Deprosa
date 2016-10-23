using depross.Model;
using System.Collections.Generic;
using System.Linq;

namespace depross.Interfaces
{
    public interface IAdvertiserRepository
    {
      Advertiser GetAdvertiser(int id);

      Advertiser GetAdvertiser(string name);

      IQueryable<Advertiser> Getadvertisers(int page, int size);

      Advertiser AddNewAdvertiser(Advertiser newAdvertiser);
      Advertiser UpdateAdvertiser(Advertiser updatedAdvertiser);

      bool IsAdvertiserInDatabase(Advertiser advertiser);

    }

    public interface IAdvertisementRepository 
    {
       IQueryable<Advertisement> GetAdvertismentsForAdvertiser(Advertiser advertiser, int page, int size);

       void AddAdvertisement(Advertisement newadvertisement);

       void UpdateAdvertisement(Advertisement updatedAdvertisement);

       void AddOrUpdateImage(int adid, Image img);
    }
}
