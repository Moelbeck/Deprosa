using deprosa.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using deprosa.Service;


namespace deprosa.WebsiteService
{
    /// <summary>
    /// Contains service calls for Area and JobCategory
    /// </summary>
    public class CategoryService
    {

        private string accountURI = string.Format("{0}{1}", Konstanter.BASEURI, "Category/");
        private HttpBaseClient client;

        public CategoryService()
        {
            client = new HttpBaseClient(accountURI);

        }
        #region GET
        public async Task<List<MainCategoryDTO>> GetAllMainCategories()
        {
            string uri = string.Format("allmain");
            var categories = await client.GetResponseObject<MainCategoryDTO, List<MainCategoryDTO>>(uri, eHttpMethodType.GET, null);
            return categories;
        }
        public async Task<MainCategoryDTO> GetMainCategory(int id)
        {
            string uri = string.Format("{0}", id);
            var categories = await client.GetResponseObject<MainCategoryDTO, MainCategoryDTO>(uri, eHttpMethodType.GET, null);
            return categories;
        }
        public async Task<List<SubCategoryDTO>> GetSubCategoriesForMain(int mainid)
        {
            string uri = string.Format("{0}/sub", mainid);
            var categories = await client.GetResponseObject<SubCategoryDTO, List<SubCategoryDTO>>(uri, eHttpMethodType.GET, null);
            return categories;
        }

        public async Task<List<MainCategoryDTO>> GetMainCategoriesBySearchString(string searchstring)
        {
            string uri = string.Format("bysearch/{0}", searchstring);
            var categories = await client.GetResponseObject<MainCategoryDTO, List<MainCategoryDTO>>(uri, eHttpMethodType.GET, null);
            return categories;
        }
        public async Task<List<ProductTypeDTO>> GetProductTypesForCategory(int id)
        {
            string uri = string.Format("sub/{0}/producttypes", id);
            var producttype = await client.GetResponseObject<ProductTypeDTO, List<ProductTypeDTO>>(uri, eHttpMethodType.GET, null);
            return producttype;
        }
        public async Task<ProductTypeDTO> GetProductTypeByID(int id)
        {

            string uri = string.Format("sub/{0}/ producttype", id);
            var producttype = await client.GetResponseObject<ProductTypeDTO, ProductTypeDTO>(uri, eHttpMethodType.GET, null);
            return producttype;
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
