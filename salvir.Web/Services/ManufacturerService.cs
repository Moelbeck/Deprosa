using depross.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using deprosa.Service;

namespace deprosa.WebsiteService
{
    public class ManufacturerService
    {
        private string manuURI = string.Format("{0}{1}", Konstanter.BASEURI, "Manufacturer/");
        private HttpBaseClient client;

        public ManufacturerService()
        {
            client = new HttpBaseClient(manuURI);
        }
        #region GET

        public async Task<List<ManufacturerDTO>> GetManufacturersInCategory(int id)
        {

            string uri = string.Format("category/{0}", id);
            var user = await client.GetResponseObject<List<ManufacturerDTO>, List<ManufacturerDTO>>(uri, eHttpMethodType.GET, null);
            return user;
        }
        public async Task<ManufacturerDTO> GetManufacturer(int id)
        {
            string uri = string.Format("{0}", id);
            var user = await client.GetResponseObject<ManufacturerDTO, ManufacturerDTO>(uri, eHttpMethodType.GET, null);
            return user;
        }
        #endregion


        #region POST
        #endregion


        #region PUT
        #endregion


        #region DELETE
        #endregion
    }
}
