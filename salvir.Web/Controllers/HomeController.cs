
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Web.Model.ViewModel;
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
            FrontPageViewModel viewModel = new FrontPageViewModel();
            viewModel.MenuViewModel = new MenuViewModel();
            viewModel.MenuViewModel.MainCategories = await _categoryService.GetAllMainCategories();
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
