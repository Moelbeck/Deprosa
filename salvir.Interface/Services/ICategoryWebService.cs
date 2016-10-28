using deprosa.ViewModel;
using System.Collections.Generic;

namespace deprosa.Interfaces
{
    public interface ICategoryWebService
    {

        #region Categories
        List<MainCategoryDTO> GetMainCategories();

        MainCategoryDTO GetMainCategory(int id);

        List<SubCategoryDTO> GetSubCategoriesForMain(int id);

 
        List<MainCategoryDTO> GetMainCategoriesBySearchString(string searchstring);
        #endregion

        #region Manufacturer

        #endregion

        void CreateMainCategory(MainCategoryDTO viewmodel);
        void CreateSubCategory(int mainid, SubCategoryDTO viewmodel);

    }
}
