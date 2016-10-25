using bzale.Service;
using depross.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bzale.WebsiteService
{
    public class RatingService
    {
        private string ratingURI= string.Format("{0}{1}", Konstanter.BASEURI, "Rating/");
        private HttpBaseClient client;

        public RatingService()
        {
            client = new HttpBaseClient(ratingURI);
        }
        #region GET
        public async Task<RatingDTO> GetMostPositiveRatingForCompany(int companyid)
        {
            string uri = string.Format("{0}/mostpositiverating", companyid);
            var categories = await client.GetResponseObject<RatingDTO, RatingDTO>(uri, eHttpMethodType.GET, null);
            return categories;
        }
        public async Task<List<RatingDTO>> GetMostPositiveRatingForCompany(int companyid, int page)
        {
            string uri = string.Format("{0}/GetRatings?page={1}&size={2}", companyid,page, Konstanter.PAGE_SIZE);
            var categories = await client.GetResponseObject<List<RatingDTO>, List<RatingDTO>>(uri, eHttpMethodType.GET, null);
            return categories;
        }
        #endregion

        #region POST
        public async Task<bool> CreateRating(RatingDTO viewmodel)
        {
            string uri = string.Format("create");
            var categories = await client.GetResponseObject<RatingDTO, bool>(uri, eHttpMethodType.POST, viewmodel);
            return categories;
        }
        #endregion
        #region PUT
        public async Task<bool> UpdateRating(RatingDTO viewmodel)
        {
            string uri = string.Format("update");
            var categories = await client.GetResponseObject<RatingDTO, bool>(uri, eHttpMethodType.PUT, viewmodel);
            return categories;
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteRating(int ratingid)
        {
            string uri = string.Format("{0}/remove",ratingid);
            var categories = await client.GetResponseObject<bool, bool>(uri, eHttpMethodType.DELETE, false);
            return categories;
        }
        #endregion
    }
}
