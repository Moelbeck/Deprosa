using depross.ViewModel;
using depross.WebService;
using salvir.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Api.Controllers
{

    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        private CategoryWebService _categoryService;
        private ProductTypeWebService _productService;
        public CategoryController()
        {
            _categoryService = new CategoryWebService();
        }

        /// <summary>
        /// Gets all categories - Get
        /// </summary>
        [HttpGet, Route("allmain")]
        public IHttpActionResult GetAllCategories()
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

        /// <summary>
        /// Gets main categories by search string
        /// </summary>
        [HttpGet, Route("bysearch/{searchString}")]       
        public IHttpActionResult GetMainCategoriesBySearchString(string searchString)
        {
            if (ModelState.IsValid)
            {
                var bysearch = _categoryService.GetMainCategoriesBySearchString(searchString);
                if (bysearch != null)
                {
                    return Ok(bysearch);
                }

                return NotFound();
            }
            return BadRequest(ModelState);
        }

        ///// <summary>
        ///// Creates main category - Post
        ///// </summary>
        //[Authorize]
        //[HttpPost, Route("create")]
        //public IHttpActionResult CreateMainCategory([FromBody]CategoryDTO viewmodel)
        //{
        //    _categoryService.CreateMainCategory(viewmodel);
        //    return Ok(true);
        //}

        ///// <summary>
        ///// Creates sub categories - Post
        ///// </summary>
        //[Authorize]
        //[HttpPost, Route("{main}/createsub")]
        //public IHttpActionResult CreateSubCategory(int main, [FromBody]CategoryDTO viewmodel)
        //{
        //    _categoryService.CreateSubCategory(main, viewmodel);
        //    return Ok(true);
        //}

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
