using System.Web;
using deprosa.Web.Data.Model.ViewModel;

namespace deprosa.Web.Data.Model.Session
{
    public class S_CategoryStructure
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
    }
}