using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Common;
using deprosa.Filter;
using deprosa.Service;
using deprosa.Services;
using deprosa.ViewModel;
using deprosa.Web.Data.Model.ViewModel;
using deprosa.Web.Model;
using deprosa.Website;

namespace deprosa.Web.Controllers
{
    public class CreateSalelistingController : Controller
    {
        // GET: CreateSalelisting
        #region Create Sale listing
        private readonly SalelistingWebService _salelistingService;
        private readonly ProductTypeWebService _categoryService;
        private readonly ImageService _imageService;

        public CreateSalelistingController()
        {
            _salelistingService = new SalelistingWebService();
            _categoryService = new ProductTypeWebService();
            _imageService = new ImageService();
        }

        [HttpGet]
        [EnsureCanSellAuthorize]
        public async Task<ActionResult> CreateSaleListing(SaleListingCreateViewModel current)
        {
            CurrentSalelisting.SaleListingViewModel = current;
            if (CurrentSalelisting.SaleListingViewModel.CategoryViewModel == null)
            {
                CategoryStructureRequest request =  _categoryService.GetCategoryStructure();
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel = new CategoryViewModel();
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SetCategoryStructure(request);
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel = CurrentSalelisting.SaleListingViewModel.CategoryViewModel;
                
            }
            if (CurrentSalelisting.SaleListingViewModel != null && CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedMainCategoryId <= 0)
            {
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel = CurrentSalelisting.SaleListingViewModel.CategoryViewModel;
                CurrentSalelisting.SetSalelistingToNew();
            }
            return View(CurrentSalelisting.SaleListingViewModel);
        }

        public ActionResult SetSelectedMainCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedMainCategoryId = categoryid;
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.CurrentSubCategories = CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SubCategories.Where(e => e.MainCategory.ID == categoryid).ToList();
            }
            else
            {
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedMainCategoryId = 0;
            }
            return View("CreateSaleListing", CurrentSalelisting.SaleListingViewModel);
        }

        public ActionResult SetSelectedSubCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedSubCategoryId = categoryid;
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.CurrentProductTypes = CurrentSalelisting.SaleListingViewModel.CategoryViewModel.ProductTypes.Where(e => e.SubCategory.ID == categoryid).ToList();

            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);

        }

        public ActionResult SetSelectedProductType(int producttypeid)
        {
            if (producttypeid > 0)
            {
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedProductTypeId = producttypeid;
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedProductType = CurrentSalelisting.SaleListingViewModel.CategoryViewModel.ProductTypes.First(e => e.ID == producttypeid);
                CurrentSalelisting.SaleListingViewModel.SaleListing = new SaleListingCreateDTO();
            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SaleListingCreateViewModel model)
        {
            model.SaleListing.ProductType = CurrentSalelisting.SaleListingViewModel.CategoryViewModel.SelectedProductType;
            ModelState.Remove("SaleListing.ProductType");
            if (ModelState.IsValid)
            {
                var salelisting = model.SaleListing;
                salelisting.Images = CurrentSalelisting.SaleListingViewModel.SaleListing.Images;
                 _salelistingService.CreateNewSaleListing(salelisting, CurrentUser.ID);
                return RedirectToAction("CompanySaleListings", "Management");

            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);

        }
        //[HttpPost]
        //public HttpResponseMessage UploadImages()
        //{
        //    var files = HttpContext.Request.Files;
        //    for (int i =0; i< files.Count;i++)
        //    {
        //        var file = files[i];
        //        if (file != null && _imageService.ValidateExtension(file.ContentType))
        //        {
        //            int filelength = file.ContentLength;
        //            byte[] imagebytes = new byte[filelength];
        //            file.InputStream.Read(imagebytes, 0, filelength);
        //            CurrentSalelisting.SaleListingViewModel.SaleListing.Images.Add(new ImageUploadDTO(){Content = imagebytes,FileName = _imageService.GenerateImageName()});
        //        }
        //    }
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}
        #endregion
    }
}