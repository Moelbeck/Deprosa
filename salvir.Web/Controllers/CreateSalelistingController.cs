using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using deprosa.Common;
using deprosa.Filter;
using deprosa.ViewModel;
using deprosa.Web.Model;
using deprosa.WebsiteService;

namespace deprosa.Web.Controllers
{
    public class CreateSalelistingController : Controller
    {
        // GET: CreateSalelisting
        #region Create Sale listing
        private SaleListingService _salelistingService;
        private readonly CategoryService _categoryService;
        public CreateSalelistingController()
        {
            _salelistingService = new SaleListingService();
            _categoryService = new CategoryService();
        }

        [HttpGet]
        [EnsureCanSellAuthorize]
        public async Task<ActionResult> CreateSaleListing(SaleListingCreateViewModel current)
        {
            CurrentSalelisting.SaleListingViewModel = current;
            if (CurrentSalelisting.SaleListingViewModel.MainCategories == null)
            {
                CategoryStructureRequest request = await _categoryService.GetCategoryStructure();
                CurrentSalelisting.SaleListingViewModel.MainCategories = request.MainCategories;
                CurrentSalelisting.SaleListingViewModel.SubCategories = request.SubCategories;
                CurrentSalelisting.SaleListingViewModel.ProductTypes = request.ProductTypes;
            }
            if (CurrentSalelisting.SaleListingViewModel.SelectedMainCategoryId <= 0)
            {
                CurrentSalelisting.SetSalelistingToNew();
            }
            return View(CurrentSalelisting.SaleListingViewModel);
        }


        public ActionResult SetSelectedMainCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.SelectedMainCategoryId = categoryid;
                CurrentSalelisting.SaleListingViewModel.CurrentSubCategories = CurrentSalelisting.SaleListingViewModel.SubCategories.Where(e => e.MainCategory.ID == categoryid).ToList();
            }
            else
            {
                CurrentSalelisting.SaleListingViewModel.SelectedMainCategoryId = 0;
            }
            return View("CreateSaleListing", CurrentSalelisting.SaleListingViewModel);
        }

        public ActionResult SetSelectedSubCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.SelectedSubCategoryId = categoryid;
                CurrentSalelisting.SaleListingViewModel.CurrentProductTypes = CurrentSalelisting.SaleListingViewModel.ProductTypes.Where(e => e.SubCategory.ID == categoryid).ToList();

            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);

        }

        public ActionResult SetSelectedProductType(int producttypeid)
        {
            if (producttypeid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.SelectedProductTypeId = producttypeid;
                CurrentSalelisting.SaleListingViewModel.SelectedProductType = CurrentSalelisting.SaleListingViewModel.ProductTypes.First(e => e.ID == producttypeid);
                CurrentSalelisting.SaleListingViewModel.SaleListing = new SaleListingDTO();
            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaleListingCreateViewModel model)
        {

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult UploadImages()
        {
            var file = HttpContext.Request.Files[0];

            // do something with the file in this space 
            // {....}
            // end of file doing

            // Now we need to wire up a response so that the calling script understands what happened
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}