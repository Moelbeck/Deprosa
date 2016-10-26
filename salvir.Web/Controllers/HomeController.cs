
using System.Diagnostics;
using System.Web.Mvc;
using deprosa.WebsiteService;

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
        public ActionResult Index()
        {
            var categories = _categoryService.GetAllMainCategories();
            return View(categories);
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
