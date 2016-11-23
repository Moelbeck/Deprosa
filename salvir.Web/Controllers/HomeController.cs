
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Web.Data.Model.Session;
using deprosa.Web.Data.Model.ViewModel;
using deprosa.WebsiteService;
using deprosaWeb.Model.ViewModel;

namespace deprosa.Web.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService _categoryService;

        public HomeController()
        {
            _categoryService = new CategoryService();
        }
        [OutputCache(Duration = 10, VaryByParam = "none")]
        public async Task<ActionResult> Index()
        {
            HighlightViewModel viewModel = new HighlightViewModel();
            viewModel.CategoryViewModel = new CategoryViewModel();
            if (!CategoryStructure.CategoryViewModel.MainCategories.Any())
            {
                var categorystructure = await _categoryService.GetCategoryStructure();
                CategoryStructure.SetCategoryStructure(categorystructure);
            }
            viewModel.CategoryViewModel = CategoryStructure.CategoryViewModel;
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
