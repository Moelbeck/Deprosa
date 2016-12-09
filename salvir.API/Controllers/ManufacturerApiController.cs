using System.Web.Http;
using deprosa.Service;

namespace WebService.Api.Controllers
{
    [RoutePrefix("api/Manufacturer")]
    public class ManufacturerApiController : ApiController
    {
        private readonly ManufacturerWebService _manufacturerservice;

        public ManufacturerApiController()
        {
            _manufacturerservice = new ManufacturerWebService();
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
