using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Web.Data.Model.Session;
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
            return View(viewModel);
        }

        public async Task<ActionResult> SetSelectedSubCategory(int selected)
        {
            HighlightViewModel viewModel = new HighlightViewModel();
            if (selected> 0)
            {
                var request = _salelistingService.GeHighlightSalelistingRequest(selected, false);
                CategoryStructure.CategoryViewModel.SelectedSubCategoryId = selected;
                CategoryStructure.CategoryViewModel.CurrentProductTypes = CategoryStructure.CategoryViewModel.ProductTypes.Where(e => e.SubCategory.ID == selected).ToList();
                viewModel.CategoryViewModel = CategoryStructure.CategoryViewModel;
                viewModel.HighligthtedSaleListings = request.Result.HighlightedSalelistings;
            }
            return View("Index", viewModel);
        }

        //public ActionResult SetSelectedProductType(int producttypeid)
        //{
        //    if (producttypeid > 0)
        //    {
        //        CategoryStructure.CategoryViewModel.SelectedProductTypeId = producttypeid;
        //        CategoryStructure.CategoryViewModel.SelectedProductType = CategoryStructure.CategoryViewModel.ProductTypes.First(e => e.ID == producttypeid);
        //        CurrentSalelisting.SaleListingViewModel.SaleListing = new SaleListingCreateDTO();
        //    }
        //    return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);
        //}
    }
}
