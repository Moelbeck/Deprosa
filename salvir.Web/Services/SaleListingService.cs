using depross.Common;
using depross.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using deprosa.Service;

namespace deprosa.WebsiteService
{
    /// <summary>
    /// This service class have connection to item repository
    /// This will be responsible for creating items
    /// Images for the items is created in ImageService
    /// </summary>
    public class SaleListingService
    {
        private string accountURI = string.Format("{0}{1}", Konstanter.BASEURI, "SaleListing/");
        private HttpBaseClient client;

        public SaleListingService()
        {
            client = new HttpBaseClient(accountURI);

        }

        #region GET
        #region Salelisting
        public async Task<SaleListingDTO> GetSaleListing(int id)
        {
            string uri = string.Format("{0}", id);
            var salelisting = await client.GetResponseObject<SaleListingDTO, SaleListingDTO>(uri, eHttpMethodType.GET, null);
            return salelisting;
        }
        public async Task<List<SaleListingDTO>> GetSaleListingsForCompany(int companyID, int page)
        {
            string uri = string.Format("company/{0}?page={1}&size={2}", companyID,page,Konstanter.PAGE_SIZE);
            var salelisting = await client.GetResponseObject<List<SaleListingDTO>, List< SaleListingDTO >> (uri, eHttpMethodType.GET, null);
            return salelisting;
        }

        public async Task<List<SaleListingDTO>> GetSaleListingsForCategory(int categoryid, int page)
        {
            string uri = string.Format("category/{0}?page={1}&size={2}", categoryid, page, Konstanter.PAGE_SIZE);
            var salelisting = await client.GetResponseObject<List<SaleListingDTO>, List<SaleListingDTO>>(uri, eHttpMethodType.GET, null);
            return salelisting;
        }

        public async Task<List<SaleListingDTO>> GetSaleListingsBySearch(string search, int page)
        {
            string uri = string.Format("getBySearch/{0}?page={1}&size={2}", search, page, Konstanter.PAGE_SIZE);
            var salelisting = await client.GetResponseObject<List<SaleListingDTO>, List<SaleListingDTO>>(uri, eHttpMethodType.GET, null);
            return salelisting;
        }
        #endregion

        #region Images

        public async Task<List<ImageDTO>> GetImages(int id)
        {
            string uri = string.Format("{0}/images", id);
            var salelisting = await client.GetResponseObject < List<ImageDTO>,  List < ImageDTO >> (uri, eHttpMethodType.GET, null);
            return salelisting;
        }
        public async Task<ImageDTO> GetImage(int id)
        {
            string uri = string.Format("{0}/image", id);
            var salelisting = await client.GetResponseObject<ImageDTO, ImageDTO>(uri, eHttpMethodType.GET, null);
            return salelisting;
        }
        #endregion


        #endregion
        #region POST
        public async Task<SaleListingDTO> CreateNewSaleListing(SaleListingDTO viewmodel)
        {
            string uri = string.Format("create");
            var salelistings = await client.GetResponseObject<SaleListingDTO, SaleListingDTO>(uri, eHttpMethodType.POST, viewmodel);
            return salelistings;
        }

        #region Images
        public async Task<bool> AddImageToSalelisting(ImageUploadRequest viewmodel)
        {
            string uri = string.Format("addImage");
            var image = await client.GetResponseObject<ImageUploadRequest, bool>(uri, eHttpMethodType.POST, viewmodel);
            return image;
        }
        #endregion

        #endregion
        #region PUT
        public async Task<bool> UpdateSaleListing(SaleListingDTO viewmodel)
        {
            string uri = string.Format("update");
            var salelistings = await client.GetResponseObject<SaleListingDTO, bool>(uri, eHttpMethodType.PUT, viewmodel);
            return salelistings;
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteSaleListing(int id)
        {
            string uri = string.Format("{0}/delete",id);
            var salelistings = await client.GetResponseObject<bool, bool>(uri, eHttpMethodType.DELETE, false);
            return salelistings;
        }
        public async Task<bool> DeleteImage(int salelistingid, int imageid)
        {
            string uri = string.Format("{0}/removeimage/{1}", salelistingid,imageid);
            var salelistings = await client.GetResponseObject<bool, bool>(uri, eHttpMethodType.DELETE, false);
            return salelistings;
        }
        #endregion


    }
}
