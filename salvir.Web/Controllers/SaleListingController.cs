using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using deprosa.ViewModel;
using deprosa.Web.Data.Model.Session;
using deprosa.Web.Data.Model.ViewModel;
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

        public async Task<ActionResult> Index(int? selected, bool issub = false, bool ispostback = false)
        {
            HighlightViewModel viewModel = new HighlightViewModel();
            if (selected != null)
            {
                int selectedid = selected.Value;
                var request = await _salelistingService.GeHighlightSalelistingRequest(selectedid, false);
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
            
            int sub = S_CategoryStructure.CategoryViewModel.SelectedSubCategoryId;
            if (sub <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            List<SaleListingDTO> salelistings = new List<SaleListingDTO>();
            if (S_CategoryStructure.CategoryViewModel.SelectedProductTypeId != 0)
            {
                salelistings = await _salelistingService.GetSaleListingsForProductType(S_CategoryStructure.CategoryViewModel.SelectedProductTypeId, sort, nextpage, search);
            }
            else
            {
                salelistings = await _salelistingService.GetSaleListingsForCategory(sub, sort, nextpage, search);
            }
            SaleListingListViewModel saleListingList = new SaleListingListViewModel
            {
                Salelistings = salelistings.ToPagedList(nextpage, int.MaxValue)
            };
            saleListingList.CategoryViewModel = S_CategoryStructure.CategoryViewModel;
            return View(saleListingList);
        }

        public async Task<ActionResult> Details(int id)
        {
            var selectedsalelisting = await _salelistingService.GetSaleListing(id);
            if (selectedsalelisting != null)
            {
                return View(selectedsalelisting);
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
