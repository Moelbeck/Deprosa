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

        public async Task<ActionResult> Index(int? selected, bool ispostback = false)
        {
            HighlightViewModel viewModel = new HighlightViewModel();
            if (selected != null)
            {
                int selectedid = selected.Value;
                var request = await _salelistingService.GeHighlightSalelistingRequest(selectedid, false);
                if (!ispostback)
                {
                    S_CategoryStructure.CategoryViewModel.SetCategoryStructure(request.CategoryStructure);
                }
                viewModel.HighligthtedSaleListings = request.HighlightedSalelistings;
                
                S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId = selectedid;
                S_CategoryStructure.CategoryViewModel.CurrentSubCategories = S_CategoryStructure.CategoryViewModel.SubCategories.Where(e => e.MainCategory.ID == selected).ToList();
                S_CategoryStructure.CategoryViewModel.CurrentProductTypes = S_CategoryStructure.CategoryViewModel.CurrentProductTypes;
                viewModel.CategoryViewModel.SetCategoryStructure(S_CategoryStructure.CategoryViewModel);
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Salelistings(string sort = "", string search = "", int? page = null)
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

            ViewBag.SelectedSub = S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId;
            
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

        public ActionResult SetSelectedSubCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId = categoryid;
                S_CategoryStructure.CategoryViewModel.CurrentProductTypes = S_CategoryStructure.CategoryViewModel.ProductTypes.Where(e => e.SubCategory.ID == categoryid).ToList();

            }
            return RedirectToAction("Index",new {selected = S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId, ispostback = true });

        }
        public ActionResult SetSelectedProductType(int producttypeid)
        {
            if (producttypeid > 0)
            {
                S_CategoryStructure.CategoryViewModel.SelectedProductTypeId = producttypeid;
                S_CategoryStructure.CategoryViewModel.SelectedProductType = S_CategoryStructure.CategoryViewModel.ProductTypes.First(e => e.ID == producttypeid);
            }
            return RedirectToAction("Index", new { selected = S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId, ispostback = true });
        }
    }
}
