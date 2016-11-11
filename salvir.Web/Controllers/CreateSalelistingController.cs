﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using deprosa.Common;
using deprosa.Filter;
using deprosa.Service;
using deprosa.ViewModel;
using deprosa.Web.Data.Model.Session;
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
        private readonly ImageService _imageService;

        public CreateSalelistingController()
        {
            _salelistingService = new SaleListingService();
            _categoryService = new CategoryService();
            _imageService = new ImageService();
        }

        [HttpGet]
        [EnsureCanSellAuthorize]
        public async Task<ActionResult> CreateSaleListing(SaleListingCreateViewModel current)
        {
            CurrentSalelisting.SaleListingViewModel = current;
            if (CategoryStructure.CategoryViewModel.MainCategories == null)
            {
                CategoryStructureRequest request = await _categoryService.GetCategoryStructure();
                CategoryStructure.SetCategoryStructure(request);
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel = CategoryStructure.CategoryViewModel;
            }
            if (CategoryStructure.CategoryViewModel.SelectedMainCategoryId <= 0)
            {
                CurrentSalelisting.SaleListingViewModel.CategoryViewModel = CategoryStructure.CategoryViewModel;
                CurrentSalelisting.SetSalelistingToNew();
            }
            return View(CurrentSalelisting.SaleListingViewModel);
        }


        public ActionResult SetSelectedMainCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CategoryStructure.CategoryViewModel.SelectedMainCategoryId = categoryid;
                CategoryStructure.CategoryViewModel.CurrentSubCategories = CategoryStructure.CategoryViewModel.SubCategories.Where(e => e.MainCategory.ID == categoryid).ToList();
            }
            else
            {
                CategoryStructure.CategoryViewModel.SelectedMainCategoryId = 0;
            }
            return View("CreateSaleListing", CurrentSalelisting.SaleListingViewModel);
        }

        public ActionResult SetSelectedSubCategory(int categoryid)
        {
            if (categoryid > 0)
            {
                CategoryStructure.CategoryViewModel.SelectedSubCategoryId = categoryid;
                CategoryStructure.CategoryViewModel.CurrentProductTypes = CategoryStructure.CategoryViewModel.ProductTypes.Where(e => e.SubCategory.ID == categoryid).ToList();

            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);

        }

        public ActionResult SetSelectedProductType(int producttypeid)
        {
            if (producttypeid > 0)
            {
                CategoryStructure.CategoryViewModel.SelectedProductTypeId = producttypeid;
                CategoryStructure.CategoryViewModel.SelectedProductType = CategoryStructure.CategoryViewModel.ProductTypes.First(e => e.ID == producttypeid);
                CurrentSalelisting.SaleListingViewModel.SaleListing = new SaleListingCreateDTO();
            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaleListingCreateViewModel model)
        {
            model.SaleListing.ProductType = CategoryStructure.CategoryViewModel.SelectedProductType;
            ModelState.Remove("SaleListing.ProductType");
            if (ModelState.IsValid)
            {
                var salelisting = model.SaleListing;
                salelisting.Images = CurrentSalelisting.SaleListingViewModel.SaleListing.Images;
                await _salelistingService.CreateNewSaleListing(salelisting);
                return RedirectToAction("CompanySaleListings", "Management");

            }
            return View("CreateSalelisting", CurrentSalelisting.SaleListingViewModel);

        }
        [HttpPost]
        public HttpResponseMessage UploadImages()
        {
            var files = HttpContext.Request.Files;
            for (int i =0; i< files.Count;i++)
            {
                var file = files[i];
                if (file != null && _imageService.ValidateExtension(file.ContentType))
                {
                    int filelength = file.ContentLength;
                    byte[] imagebytes = new byte[filelength];
                    file.InputStream.Read(imagebytes, 0, filelength);
                    CurrentSalelisting.SaleListingViewModel.SaleListing.Images.Add(new ImageUploadDTO(){Content = imagebytes,FileName = _imageService.GenerateImageName()});
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        #endregion
    }
}