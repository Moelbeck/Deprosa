using depross.Common;
using depross.ViewModel;
using depross.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Api.Controllers
{
    [RoutePrefix("api/SaleListing")]
    public class SaleListingController : ApiController
    {
        private SaleListingWebService _salelistingService;

        public SaleListingController()

        {
            _salelistingService = new SaleListingWebService();
        }
        // GET: api/values
        /// <summary>
        /// Create new sale listing - Post
        /// </summary>
        #region Salelisting
        [Authorize]
        [HttpPost,Route("create")]
        public IHttpActionResult CreateNewSaleListing([FromBody]SaleListingDTO model)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.CreateNewSaleListing(model))
                {
                    return Ok();
                }
                return BadRequest("Annonce blev ikke lavet");
            }
            return BadRequest(ModelState);
        }

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
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        ///Delete sale listing - Delete 
        /// </summary>
        [Authorize]
        [HttpDelete,Route("{saleID}/delete")]
        public IHttpActionResult DeleteSaleListingByID(int saleID)
        {
            if (_salelistingService.DeleteSaleListingByID(saleID))
            {
                return Ok(true);
            }
            return BadRequest("Annonce blev ikke slettet");
        }

        /// <summary>
        /// Update sale listing - Put
        /// </summary>
        [Authorize]
        [HttpPut,Route("update")]
        public IHttpActionResult UpdateSaleListing([FromBody]SaleListingDTO viewmodel)
        {
            if (ModelState.IsValid)
            {
                if (_salelistingService.UpdateSaleListing(viewmodel))
                {
                    return Ok(true);
                }
                return BadRequest("Annonce blev ikke opdateret");
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get sale listings for company - Get
        /// </summary>
        [HttpGet,Route("company/{companyID}")]
        public IHttpActionResult GetSaleListingsForCompany(int companyID, string sort, bool isAsc, int page, int size)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetSaleListingsForCompany(companyID, sort,isAsc,page,size);
                if (salelisting != null)
                {
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get sale listings for category - Get
        /// </summary>
        [HttpGet,Route("category/{categoryID}")]
        public IHttpActionResult GetSaleListingsForCategory(int categoryID, string sort, bool isAsc, int page, int size)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetSaleListingsForCategory(categoryID, sort, isAsc, page, size);
                if (salelisting != null)
                {
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Get sale listing by search string - Get
        /// </summary>
        [HttpGet,Route("getBySearch/{search}")]
        public IHttpActionResult GetSaleListingsBySearchString(string search, string sort, bool isAsc, int page, int size)
        {
            if (ModelState.IsValid)
            {
                var salelisting = _salelistingService.GetSaleListingsBySearchString(search, sort, isAsc, page, size);
                if (salelisting != null)
                {
                    return Ok(salelisting);
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        #endregion
        #region Images

        /// <summary>
        /// Add image to sale listing - Post
        /// </summary>
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
