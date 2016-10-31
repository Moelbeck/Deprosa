using deprosa.Common;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            viewModel.MenuViewModel.SelectedMainCategory = viewModel.MenuViewModel.SubCategories.First(e=>e.MainCategory.ID == selected).MainCategory;

            return View(viewModel);
        }

        #region Create Sale listing

        [HttpGet]
        [EnsureCanSellAuthorize]
        public async Task<ActionResult> CreateSaleListing(SaleListingCreateViewModel current)
        {
            CurrentSalelisting.SaleListingViewModel = current;
            if(CurrentSalelisting.SaleListingViewModel.MainCategories == null)
            {
                CategoryStructureRequest request = await _categoryService.GetCategoryStructure();
                CurrentSalelisting.SaleListingViewModel.MainCategories = request.MainCategories;
                CurrentSalelisting.SaleListingViewModel.SubCategories = request.SubCategories;
                CurrentSalelisting.SaleListingViewModel.ProductTypes = request.ProductTypes;
            }
            if(CurrentSalelisting.SaleListingViewModel.SelectedMainCategory == null)
            {
                CurrentSalelisting.SetSalelistingToNew();
            }
            return View(CurrentSalelisting.SaleListingViewModel);
        }


        public ActionResult SetSelectedMainCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.SelectedMainCategory = CurrentSalelisting.SaleListingViewModel.MainCategories.FirstOrDefault(e => e.ID == categoryid);
                CurrentSalelisting.SaleListingViewModel.CurrentSubCategories = CurrentSalelisting.SaleListingViewModel.SubCategories.Where(e => e.MainCategory.ID == categoryid).ToList();
            }
            else
            {
                CurrentSalelisting.SaleListingViewModel.SelectedMainCategory = null;
            }
            return View("CreateSaleListing", CurrentSalelisting.SaleListingViewModel);
        }

        public ActionResult SetSelectedSubCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.SelectedSubCategory
                    = CurrentSalelisting.SaleListingViewModel.SubCategories.FirstOrDefault(e => e.ID == categoryid);
                CurrentSalelisting.SaleListingViewModel.CurrentProductTypes = CurrentSalelisting.SaleListingViewModel.ProductTypes.Where(e => e.SubCategory.ID == categoryid).ToList();

            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);

        }

        public ActionResult SetSelectedProductType(int producttypeid)
        {
            if (producttypeid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.SelectedProductType
                    = CurrentSalelisting.SaleListingViewModel.ProductTypes.FirstOrDefault(e => e.ID == producttypeid);
                CurrentSalelisting.SaleListingViewModel.SaleListing = new SaleListingDTO();
            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaleListingCreateViewModel model)
        {   
            return RedirectToAction("Index","Home");
        }
            #endregion


        }
}
