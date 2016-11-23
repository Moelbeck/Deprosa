using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Web.Data.Model.Session;
using deprosa.Web.Model;
using deprosa.WebsiteService;
using deprosaWeb.Model.ViewModel;
using PagedList;

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

            if (CategoryStructure.CategoryViewModel.MainCategories == null)
            {
                var request = await _salelistingService.GeHighlightSalelistingRequest(selected, false);
                CategoryStructure.SetCategoryStructure(request.CategoryStructure);
                viewModel.HighligthtedSaleListings = request.HighlightedSalelistings;
            }
            CategoryStructure.CategoryViewModel.SelectedMainCategoryId = selected;
            if (!viewModel.HighligthtedSaleListings.Any())
            {
                viewModel.HighligthtedSaleListings = await _salelistingService.GetPopularForMain(selected);
            }
            viewModel.CategoryViewModel = CategoryStructure.CategoryViewModel;
            viewModel.CategoryViewModel.CurrentSubCategories =
                viewModel.CategoryViewModel.SubCategories.Where(e => e.MainCategory.ID == selected).ToList();
            return View(viewModel);
        }

        public async Task<ActionResult> Salelistings(int selectedsub = 0, string sort = "", string search = "", int? page = null)
        {
            ViewBag.CurrentSort = sort;
            ViewBag.DateSortParm = sort = String.IsNullOrWhiteSpace(sort) ? "created_desc" : "";
            ViewBag.TitleSortParam = sort == "title" ? "title_desc" : "title";
            ViewBag.PriceSortParm = sort == "price" ? "price_desc" : "price";
            int nextpage = 0;
            if (page != null)
            {
                nextpage = (int) page;
            }
            if (search != null)
            {
                nextpage = 1;
            }
            if (selectedsub > 0)
            {
                ViewBag.SelectedSub = selectedsub;
            }
            int sub = ViewBag.SelectedSub;
            if (sub <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var salelistings = await _salelistingService.GetSaleListingsForCategory(sub, sort, nextpage, search);
            SaleListingListViewModel saleListingList = new SaleListingListViewModel
            {
                Salelistings = salelistings.ToPagedList(nextpage, int.MaxValue)
            };
            return View(saleListingList);
        }
    }
}
