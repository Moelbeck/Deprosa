using deprosa.ViewModel;
using deprosa.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using deprosa.WebApi.Services;
using deprosa.Common;
using deprosa.service;

namespace WebService.Api.Controllers
{

    [RoutePrefix("api/Category")]
    public class CategoryApiController : ApiController
    {
        private readonly CategoryApiService _categoryService;
        private readonly ProductTypeApiService _productService;
        private readonly LogService _log;
        public CategoryApiController()
        {
            _categoryService = new CategoryApiService();
            _productService = new ProductTypeApiService();
            _log = new LogService();
        }

        /// <summary>
        /// Gets all categories - Get
        /// </summary>
        [HttpGet, Route("allmain")]
        public IHttpActionResult GetAllMainCategories()
        {

            var categories = _categoryService.GetMainCategories();
            if (categories.Any())
            {
                return Ok(categories);
            }
            return NotFound();
        }

        /// <summary>
        /// Gets main category by id - Get
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetMainCategory(int id)
        {
            var category = _categoryService.GetMainCategory(id);
            if (category != null)
            {
                int userid;
                if (int.TryParse(Thread.CurrentPrincipal.Identity.Name, out userid) && userid > 0)
                {
                    _log.LogCategory(userid,id,0);
                }
                return Ok(category);
            }
            return NotFound();
        }

        /// <summary>
        /// Gets sub categories for main id - Get
        /// </summary>
        [HttpGet, Route("{mainid}/sub")]
        public IHttpActionResult GetSubCategoriesForMain(int mainid)
        {
            var sub = _categoryService.GetSubCategoriesForMain(mainid);
            if (sub != null)
            {
                return Ok(sub);
            }
            return NotFound();
        }

        [HttpGet, Route("categorystructure")]
        public IHttpActionResult GetCategoryStructure()
        {
            var categorystructure = _productService.GetCategoryStructure();
            if (categorystructure != null)
            {
                return Ok(categorystructure);
            }
            return BadRequest();
        }

        #region ProductTypes
        /// <summary>
        /// Get product types for sub category - Get
        /// </summary>
        [HttpGet, Route("sub/{subcategory}/producttypes")]
        public IHttpActionResult GetProductTypesForSubCategory(int subcategory)
        {
            var producttypes = _productService.GetProductTypesForSubCategory(subcategory);
            if (producttypes != null)
            {
                return Ok(producttypes);
            }
            return NotFound();
        }

        /// <summary>
        /// Get product type by id - Get
        /// </summary>
        [HttpGet, Route("sub/{id}/producttype")]
        public IHttpActionResult GetProductType(int id)
        {
            var producttypes = _productService.GetProdyctType(id);
            if (producttypes != null)
            {
                return Ok(producttypes);
            }
            return NotFound();
        }
        #endregion
    }
}
