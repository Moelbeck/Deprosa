using deprosa.Common;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using deprosa.Filter;
using deprosa.Web.Model;
using deprosa.Web.Model.ViewModel;
using deprosa.WebsiteService;
using deprosaWeb.Model.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace deprosa.Web.Controllers
{
    public class SaleListingController : Controller
    {
        // GET: /<controller>/
        private SaleListingService _salelistingService;
        private readonly CategoryService _categoryService;
        public SaleListingController()
        {
            _salelistingService = new SaleListingService();
            _categoryService = new CategoryService();
        }

        public async Task<ActionResult> Index(int selected)
        {
            HighlightViewModel viewModel = new HighlightViewModel();
            viewModel.MenuViewModel = new MenuViewModel();
            viewModel.MenuViewModel.SubCategories = await _categoryService.GetSubCategoriesForMain(selected);
            viewModel.MenuViewModel.SelectedMainCategory = viewModel.MenuViewModel.SubCategories.First(e => e.MainCategory.ID == selected).MainCategory;

            return View(viewModel);
        }
    }
}
