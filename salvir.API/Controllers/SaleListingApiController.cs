using deprosa.Common;
using deprosa.ViewModel;
using deprosa.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using deprosa.API.Authenticator;
using deprosa.Common.RequestWrappers;
using deprosa.service;
using deprosa.WebApi.Services;

namespace WebService.Api.Controllers
{
    [RoutePrefix("api/SaleListing")]
    public class SaleListingApiController : ApiController
    {
        private readonly SaleListingApiService _salelistingService;
        private readonly ProductTypeApiService _productTypeApiService;
        private LogService _log;
        public SaleListingApiController()
        {
            _salelistingService = new SaleListingApiService();
            _log = new LogService();
            _productTypeApiService = new ProductTypeApiService();
        }
        #region Salelisting

        /// <summary>
        /// Get sale listing by id - Get
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetSaleListingByID(int id)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetSaleListingByID(id);
                if (salelisting != null)
                {
                    var accountname = Thread.CurrentPrincipal.Identity.Name;
                    int userid;
                    if (int.TryParse(accountname, out userid) && userid > 0)
                    {
                        int mainid = salelisting.ProductType.SubCategory.MainCategory.ID;
                        int subid = salelisting.ProductType.SubCategoryID;
                        _log.LogSaleListing(userid,eLogSaleListingType.Search,mainid,subid,id);
                    }
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpGet,Route("popularsub/{subid}")]
        public IHttpActionResult GetPopularForSub(int subid)
        {
            if (ModelState.IsValid)
            {
                var popularlogged = _log.GetPopularSalelistingsForSub(subid);
                var popularsale = _salelistingService.GetPopular(popularlogged,subid,true);
                return Ok(popularsale);
            }
            return BadRequest(ModelState);
        }
        [HttpGet, Route("popularmain/{mainid}")]
        public IHttpActionResult GetPopularForMain(int mainid)
        {
            if (ModelState.IsValid)
            {
                var popularlogged = _log.GetPopularSalelistingsForMain(mainid);
                var popularsale = _salelistingService.GetPopular(popularlogged,mainid,false);
                return Ok(popularsale);
            }
            return BadRequest(ModelState);
        }
        [HttpGet, Route("popularuser/{userid}")]
        public IHttpActionResult GetPopularForUser(int userid)
        {
            if (ModelState.IsValid)
            {
                var popularlogged = _log.GetPopularSalelistingsForUser(userid);
                var popularsale = _salelistingService.GetPopular(popularlogged,0,false);
                return Ok(popularsale);
            }
            return BadRequest(ModelState);
        }

        [HttpGet, Route("gethighlighted/{categoryid}/{isSub}")]
        public IHttpActionResult GeHighlightSalelistingRequest(int categoryid, bool isSub)
        {
            if (ModelState.IsValid)
            {
                HighlightSalelistingRequest request = new HighlightSalelistingRequest();
                List<int> popularlogged = new List<int>();
                popularlogged = !isSub
                    ? _log.GetPopularSalelistingsForMain(categoryid)
                    : _log.GetPopularSalelistingsForSub(categoryid);
                var popularsale = _salelistingService.GetPopular(popularlogged,categoryid,isSub);
                request.HighlightedSalelistings = popularsale;
                request.CategoryStructure = _productTypeApiService.GetCategoryStructure();
                //request.CategoryStructure.SubCategories = !isSub
                //    ? _categoryService.GetSubCategoriesForMain(categoryid)
                //    : new List<SubCategoryDTO>();
                //var accountname = Thread.CurrentPrincipal.Identity.Name;
                //int userid;
                //if (int.TryParse(accountname, out userid) && userid > 0)
                //{
                //    int mainid = !isSub ? categoryid : 0;
                //    int subid =  isSub ? categoryid : 0;
                //    _log.LogCategory(userid, mainid, subid);
                //}
                return Ok(request);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get sale listings for company - Get
        /// </summary>
        [HttpGet, Route("company/{vat}")]
        public IHttpActionResult GetSaleListingsForCompany(string vat, string sort, int page, int size, string search = null)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetForCompany(vat, search, sort, page, size);
                if (salelisting != null)
                {
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpGet, Route("following/{userid}")]
        public IHttpActionResult GetFollowingSalelistings(int userid, string sort, int page, int size, string search = null)
        {
            var salelistings = _salelistingService.GetFollowingSalelistings(userid, sort, page, size, search);
            if (salelistings != null)
            {
                return Ok(salelistings);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get sale listings for category - Get
        /// </summary>
        [HttpGet, Route("category/{categoryID}")]
        public IHttpActionResult GetSaleListingsForCategory(int categoryID, string sort, int page, int size, string search)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetForSubCategory(categoryID, search, sort, page, size);
                if (salelisting != null)
                {
                    int userid;
                    if (int.TryParse(Thread.CurrentPrincipal.Identity.Name, out userid) && userid > 0)
                    {
                        _log.LogCategory(userid, 0, categoryID);
                    }
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get sale listing by search string - Get
        /// </summary>
        [HttpGet, Route("getBySearch/{search}")]
        public IHttpActionResult GetSaleListingsBySearchString(string search, string sort, int page, int size)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetBySearchString(search, sort, page, size);
                if (salelisting != null)
                {
                    int userid;
                    if (int.TryParse(Thread.CurrentPrincipal.Identity.Name, out userid) && userid > 0)
                    {
                        _log.LogSearch(userid, search);
                    }
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // GET: api/values
        /// <summary>
        /// Create new sale listing - Post
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpPost, Route("create")]
        public IHttpActionResult CreateNewSaleListing([FromBody]SaleListingCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var accountname = Thread.CurrentPrincipal.Identity.Name;
                int userid;
                if (int.TryParse(accountname, out userid) && userid > 0)
                {
                    if (_salelistingService.CreateNewSaleListing(model, userid))
                    {
                        int mainid = model.ProductType.SubCategory.MainCategory.ID;
                        int subid = model.ProductType.SubCategoryID;
                        _log.LogSaleListing(userid, eLogSaleListingType.Created, mainid, subid, 0);
                        return Ok();
                    }
                }
                return BadRequest("Annonce blev ikke lavet");
            }
            return BadRequest(ModelState);
        }
        /// <summary>
        ///Delete sale listing - Delete 
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpDelete,Route("{saleID}/delete")]
        public IHttpActionResult DeleteSaleListingByID(int saleID)
        {
            if (_salelistingService.DeleteSaleListingByID(saleID))
            {
                var accountname = Thread.CurrentPrincipal.Identity.Name;
                int userid;
                if (int.TryParse(accountname, out userid) && userid > 0)
                {
                    _log.LogSaleListing(userid, eLogSaleListingType.Deleted, 0, 0, saleID);
                }
                return Ok(true);
            }
            return BadRequest("Annonce blev ikke slettet");
        }

        /// <summary>
        /// Update sale listing - Put
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpPut,Route("update")]
        public IHttpActionResult UpdateSaleListing([FromBody]SaleListingDTO viewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.UpdateSaleListing(viewmodel))
                {
                    var accountname = Thread.CurrentPrincipal.Identity.Name;
                    int userid;
                    if (int.TryParse(accountname, out userid) && userid > 0)
                    {
                        int mainid = viewmodel.ProductType.SubCategory.MainCategory.ID;
                        int subid = viewmodel.ProductType.SubCategoryID;
                        _log.LogSaleListing(userid, eLogSaleListingType.Update, mainid, subid, viewmodel.ID);
                    }
                    return Ok(true);
                }
                return BadRequest("Annonce blev ikke opdateret");
            }
            return BadRequest(ModelState);
        }

        #endregion
        #region Images

        /// <summary>
        /// Add image to sale listing - Post
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpPost,Route("addImage")]
        public IHttpActionResult AddImageSaleListing([FromBody]ImageUploadRequest viewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.AddImageSaleListing(viewmodel.SaleListing, viewmodel.Image))
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Remove image from sale listing - Delete
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpDelete,Route("{salelistingid}/removeimage/{id}")]
        public IHttpActionResult RemoveImageSaleListing(int salelistingid, int imageid)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.RemoveImageSaleListing(salelistingid, imageid))
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get images for sale listing - Get
        /// </summary>
        [HttpGet,Route("{salelistingid}/images")]
        public IHttpActionResult GetImagesForSaleListing(int salelistingid)
        {
            if (ModelState.IsValid)
            {
                var images = _salelistingService.GetImagesForSaleListing(salelistingid);
                if (images != null)
                {
                    return Ok(images);
                }

                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get single image for sale listing - Get
        /// </summary>
        [HttpGet,Route("{salelistingid}/image")]
        public IHttpActionResult GetImageForSaleListing(int salelistingid)
        {
            if (ModelState.IsValid)
            {
                var image = _salelistingService.GetImageForSaleListing(salelistingid);
                if (image != null)
                {
                    return Ok(image);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Subscription

        /// <summary>
        /// Add or update a subscription for a sale listing - Post
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpPost,Route("subscription/{sub}")]
        public IHttpActionResult AddOrUpdateSubscription(eSubscription sub, [FromBody] SaleListingDTO salelistingviewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.AddOrUpdateSubscription(sub, salelistingviewmodel))
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region Comments

        /// <summary>
        /// Add a comment to a sale listing- Post
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpPost,Route("{salelistingid}/addcomment")]
        public IHttpActionResult AddComment(int salelistingid, [FromBody]CommentDTO commentviewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.AddComment(salelistingid, commentviewmodel))
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Add answer for a comment - Post
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpPost,Route("{salelistingid}/comments/{commentid}/AddAnswer")]
        public IHttpActionResult AddAnswerForComment(int salelistingid, int commentid, [FromBody] CommentDTO answerviewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.AddAnswerForComment(salelistingid, commentid, answerviewmodel))
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a comment - Delete
        /// </summary>
        [EnsureCanSellAuthorize]
        [HttpDelete,Route("removeComment/{id}")]
        public IHttpActionResult RemoveComment(int id, [FromBody]SaleListingDTO saleviewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.RemoveComment(saleviewmodel, id))
                {
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Gets comments for sale listing - Get
        /// </summary>
        [HttpGet,Route("comments/{salelistingID}")]
        public IHttpActionResult GetCommentsForSaleListing(int salelistingID)
        {
            if (ModelState.IsValid)
            {
                var comments = _salelistingService.GetCommentsForSaleListing(salelistingID);

                if (comments != null)
                {
                    return Ok(comments);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        #endregion
    }
}
