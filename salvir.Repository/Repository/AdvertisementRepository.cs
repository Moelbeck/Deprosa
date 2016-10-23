using depross.Interfaces;
using depross.Model;
using depross.Repository.Abstract;
using depross.Repository.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depross.Repository
{
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(BzaleDatabaseContext context) : base(context)
        {

        }
        public IQueryable<Advertisement> GetAdvertismentsForAdvertiser(Advertiser advertiser, int page, int size)
        {
            return Get(e => e.Advertiser.ID == advertiser.ID);
        }


        public void AddAdvertisement(Advertisement newadvertisement)
        {
            Add(newadvertisement);
            Save();

        }

        public void UpdateAdvertisement(Advertisement updatedAdvertisement)
        {
            Edit(updatedAdvertisement);
            Save();
        }

        public void AddOrUpdateImage(int adid, Image img)
        {
            var ad = GetSingle(e => e.ID == adid);
            ad.Image = img;
            Edit(ad);
            Save();
        }
    }

}
