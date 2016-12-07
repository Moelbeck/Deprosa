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
            HighlightViewModel viewModel = new HighlightViewModel {CategoryViewModel = new CategoryViewModel()};
            if (!S_CategoryStructure.CategoryViewModel.MainCategories.Any())
            {
                var categorystructure = await _categoryService.GetCategoryStructure();
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
