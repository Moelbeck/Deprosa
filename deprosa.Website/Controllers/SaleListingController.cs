using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.Common.RequestWrappers;
using deprosa.Service;
using deprosa.Services;
using deprosa.ViewModel;
using deprosa.Web.Data.Model.Session;
using deprosa.Web.Data.Model.ViewModel;
using deprosa.Website.Controllers;
using deprosa.Website.Data.Model.ViewModel;
using deprosa.WebsiteService;
using deprosaWeb.Model.ViewModel;
using PagedList;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace deprosa.Web.Controllers
{
    public class SaleListingController : BaseController
    {
        // GET: /<controller>/
        private SalelistingWebService _salelistingService;
        private readonly ProductTypeWebService productTypeWebService;

        public SaleListingController()
        {
            _salelistingService = new SalelistingWebService();
            productTypeWebService = new ProductTypeWebService();
        }

        public async Task<ActionResult> Index(int? selected, bool issub = false, bool ispostback = false)
        {
            HighlightViewModel viewModel = new HighlightViewModel();
            if (selected != null)
            {
                int selectedid = selected.Value;
                
                HighlightSalelistingRequest request = new HighlightSalelistingRequest();
                request.HighlightedSalelistings = _salelistingService.GetPopular(selectedid, issub);
                request.CategoryStructure = productTypeWebService.GetCategoryStructure();
                if (!ispostback && !issub)
                {
                    S_CategoryStructure.CategoryViewModel.SelectedProductTypeId = 0;
                    S_CategoryStructure.CategoryViewModel.SelectedProductType = null;
                    S_CategoryStructure.CategoryViewModel.SetCategoryStructure(request.CategoryStructure);
                }
                viewModel.HighligthtedSaleListings = request.HighlightedSalelistings;
                if (issub)
                {
                    S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId = selectedid;
                    S_CategoryStructure.CategoryViewModel.CurrentProductTypes = S_CategoryStructure.CategoryViewModel.ProductTypes.Where(e => e.SubCategory.ID == selectedid).ToList();

                }
                else
                {
                    S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId = selectedid;
                }
                S_CategoryStructure.CategoryViewModel.CurrentSubCategories = S_CategoryStructure.CategoryViewModel.SubCategories.Where(e => e.MainCategory.ID == S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId).ToList();
                S_CategoryStructure.CategoryViewModel.CurrentProductTypes = S_CategoryStructure.CategoryViewModel.CurrentProductTypes;
                viewModel.CategoryViewModel.SetCategoryStructure(S_CategoryStructure.CategoryViewModel);
                return View(viewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public  ActionResult Salelistings(string sort = "", string search = "", int? page = null)
        {
            int nextpage = Sort(sort, search, page);
            
            int sub = S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId;
            if (sub <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            List<SaleListingDTO> salelistings = new List<SaleListingDTO>();
            if (S_CategoryStructure.CategoryViewModel.SelectedProductTypeId != 0)
            {
                salelistings =  _salelistingService.GetForProductType(S_CategoryStructure.CategoryViewModel.SelectedProductTypeId, sort, nextpage, search);
            }
            else
            {
                salelistings =  _salelistingService.GetForSubCategory(sub, sort, nextpage, search);
            }
            SaleListingListViewModel saleListingList = new SaleListingListViewModel
            {
                Salelistings = salelistings.ToPagedList(nextpage, int.MaxValue)
            };
            saleListingList.CategoryViewModel = S_CategoryStructure.CategoryViewModel;
            return View(saleListingList);
        }

        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                var salelistingid = (int) id;

                var selectedsalelisting = _salelistingService.GetSaleListingByID(salelistingid);
                if (selectedsalelisting != null)
                {
                    return View(selectedsalelisting);
                }
            }
            return RedirectToAction("Index", new { selected = S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId });

        }

        public ActionResult SetSelectedSubCategory(int? categoryid)
        {
            if (categoryid != null)
            {
                var category = (int) categoryid;
                S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId = category;
                S_CategoryStructure.CategoryViewModel.CurrentProductTypes =
                    S_CategoryStructure.CategoryViewModel.ProductTypes.Where(e => e.SubCategory.ID == categoryid)
                        .ToList();

            }
            else
            {
                S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId = 0;
                S_CategoryStructure.CategoryViewModel.SelectedProductTypeId = 0;
                S_CategoryStructure.CategoryViewModel.SelectedProductType = null;
            }
            return RedirectToAction("Index",new {selected = S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId, ispostback = true });

        }
        public ActionResult SetSelectedProductType(int? producttypeid)
        {
            if (producttypeid != null)
            {
                var product = (int) producttypeid;
                S_CategoryStructure.CategoryViewModel.SelectedProductTypeId = product;
                S_CategoryStructure.CategoryViewModel.SelectedProductType =
                    S_CategoryStructure.CategoryViewModel.ProductTypes.First(e => e.ID == producttypeid);
            }
            else
            {
                S_CategoryStructure.CategoryViewModel.SelectedProductTypeId = 0;
                S_CategoryStructure.CategoryViewModel.SelectedProductType = null;
            }
            return RedirectToAction("Index", new { selected = S_CategoryStructure.CategoryViewModel.SelectedMainCategoryId, ispostback = true });
        }
    }
}
