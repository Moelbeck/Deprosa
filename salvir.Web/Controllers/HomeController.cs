using System.Linq;
using System.Web.Mvc;
using deprosa.Web.Data.Model.Session;
using deprosa.Web.Data.Model.ViewModel;
using deprosa.WebApi.Services;
using deprosaWeb.Model.ViewModel;

namespace deprosa.Web.Controllers
{
    public class HomeController : Controller
    {
        private ProductTypeWebService _productWebService;

        public HomeController()
        {
            _productWebService = new ProductTypeWebService();
        }
        [OutputCache(Duration = 10, VaryByParam = "none")]
        public ActionResult Index()
        {
            HighlightViewModel viewModel = new HighlightViewModel {CategoryViewModel = new CategoryViewModel()};
            if (!S_CategoryStructure.CategoryViewModel.MainCategories.Any())
            {
                var categorystructure =  _productWebService.GetCategoryStructure();
                S_CategoryStructure.CategoryViewModel.SetCategoryStructure(categorystructure);
            }
            viewModel.CategoryViewModel = S_CategoryStructure.CategoryViewModel;
            return View(viewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
