using System.Web;
using deprosa.Common;
using deprosa.Web.Data.Model.ViewModel;

namespace deprosa.Web.Data.Model.Session
{
    public class CategoryStructure
    {
        public static CategoryViewModel CategoryViewModel
        {
            get
            {
                CategoryViewModel model = HttpContext.Current.Session["CategoryViewModel"] as CategoryViewModel;
                if (model == null)
                {
                    model = new CategoryViewModel();
                    HttpContext.Current.Session["CategoryViewModel"] = model;
                }
                return model;
            }
            set
            {
                HttpContext.Current.Session["CategoryViewModel"] = value;
            }
        }

        public static void SetCategoryStructure(CategoryStructureRequest categorystructure)
        {
            CategoryViewModel.MainCategories = categorystructure.MainCategories;
            CategoryViewModel.SubCategories = categorystructure.SubCategories;
            CategoryViewModel.ProductTypes = categorystructure.ProductTypes;
            CategoryViewModel.SelectedMainCategoryId = 0;
            CategoryViewModel.SelectedSubCategoryId = 0;
            CategoryViewModel.SelectedProductTypeId = 0;
        }
    }
}