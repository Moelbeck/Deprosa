using deprosa.Common;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Filter;
using deprosa.Web.Model;
using deprosa.WebsiteService;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace deprosa.Web.Controllers
{
    [EnsureCanSellAuthorize]
    public class SaleListingController : Controller
    {
        // GET: /<controller>/
        private SaleListingService _salelistingService;
        private CategoryService _categoryService;
        public SaleListingController()
        {
            _salelistingService = new SaleListingService();
            _categoryService = new CategoryService();
        }

        public ActionResult Index()
        {

            return View();
        }


        #region Create Sale listing

        [HttpGet]
        [EnsureCanSellAuthorize]
        public async Task<ActionResult> CreateSaleListing()
        {
            CurrentSalelistingCreate.SaleListingViewModel = null;

            CurrentSalelistingCreate.SaleListingViewModel.MainCategories = await _categoryService.GetAllMainCategories();
            return View(CurrentSalelistingCreate.SaleListingViewModel);
        }


        public async Task<ActionResult> SetSelectedMainCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelistingCreate.SaleListingViewModel.SelectedMainCategory
                    = CurrentSalelistingCreate.SaleListingViewModel.MainCategories.FirstOrDefault(e => e.ID == categoryid);
                List<CategoryDTO> subcategories = await _categoryService.GetSubCategoriesForMain(categoryid);
                CurrentSalelistingCreate.SaleListingViewModel.SubCategories = subcategories;
            }
            return PartialView("CreateSalelisting", CurrentSalelistingCreate.SaleListingViewModel);

        }

        public async Task<ActionResult> SetSelectedSubCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelistingCreate.SaleListingViewModel.SelectedSubCategory
                    = CurrentSalelistingCreate.SaleListingViewModel.SubCategories.FirstOrDefault(e => e.ID == categoryid);
                CurrentSalelistingCreate.SaleListingViewModel.ProductTypes = await _categoryService.GetProductTypesForCategory(categoryid);
            }
            return PartialView("CreateSalelisting", CurrentSalelistingCreate.SaleListingViewModel);

        }

        public ActionResult SetSelectedProductType(int producttypeid)
        {
            if (producttypeid > 0)
            {
                CurrentSalelistingCreate.SaleListingViewModel.SelectedProductType
                    = CurrentSalelistingCreate.SaleListingViewModel.ProductTypes.FirstOrDefault(e => e.ID == producttypeid);
                CurrentSalelistingCreate.SaleListingViewModel.SaleListing = new SaleListingDTO();
            }
            return PartialView("CreateSalelisting", CurrentSalelistingCreate.SaleListingViewModel);

        }

        #endregion


    }
}
