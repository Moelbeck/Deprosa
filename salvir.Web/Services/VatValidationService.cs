using depross.ViewModel;
using System.Threading.Tasks;
using deprosa.Service;

namespace deprosa.WebsiteService
{
    public class VatValidationService
    {
        private string URI = string.Format("{0}{1}", Konstanter.BASEURI, "vat");
        private HttpBaseClient client;
        public VatValidationService()
        {
            client = new HttpBaseClient(URI);
        }

        #region GET
        public async Task<VatValidationDTO> GetVatValidation(string countrycode, string vatnr)
        {
            string uri = string.Format("?cc={0}&vatnr={1}", countrycode,vatnr);
            var vatvalidation = await client.GetResponseObject<VatValidationDTO, VatValidationDTO>(uri, eHttpMethodType.GET, null);
            return vatvalidation;
        }
        #endregion
    }
}
