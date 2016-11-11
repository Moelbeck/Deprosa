using deprosa.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebService.Api.Controllers
{
    [RoutePrefix("api/Manufacturer")]
    public class ManufacturerApiController : ApiController
    {
        private ManufacturerApiService _manufacturerservice;

        public ManufacturerApiController()
        {
            _manufacturerservice = new ManufacturerApiService();
        }
        /// <summary>
        /// Get manufacturers in category - Get
        /// </summary>
        [HttpGet, Route("category/{id}")]
        public IHttpActionResult GetManufacturersInCategory(int id)
        {

            var manu = _manufacturerservice.GetManufacturersInCategory(id);
            if (manu != null)
            {
                return Ok(manu);
            }
            return NotFound();
        }
        /// <summary>
        /// Get manufacturer by id - Get
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetManufacturer(int id)
        {
            var manu = _manufacturerservice.GetManuFacturer(id);
            if (manu != null)
            {
                return Ok(manu);
            }
            return NotFound();
        }
    }
}
