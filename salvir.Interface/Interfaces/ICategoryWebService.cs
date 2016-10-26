using deprosa.ViewModel;
using System.Collections.Generic;

namespace deprosa.Interfaces
{
    public interface ICategoryWebService
    {

        #region Categories
        List<CategoryDTO> GetMainCategories();
 
        CategoryDTO GetMainCategory(int id);

        List<CategoryDTO> GetSubCategoriesForMain(int id);

 
        List<CategoryDTO> GetMainCategoriesBySearchString(string searchstring);
        #endregion

        #region Manufacturer

        #endregion

        void CreateMainCategory(CategoryDTO viewmodel);
        void CreateSubCategory(int mainid,CategoryDTO viewmodel);

    }
}
