using bzale.Filter;
using bzale.Web.Model;
using bzale.WebsiteService;
using depross.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace bzale.Web.Controllers
{
    [EnsureCanSellAuthorize]
    public class SaleListingController : Controller
    {
        // GET: /<controller>/
        private SaleListingService _salelistingService;
        private CategoryService _categoryService;
        public SaleListingController()
        {
            _salelistingService = new SaleListingService();
            _categoryService = new CategoryService();
        }

        public ActionResult Index()
        {

            return View();
        }
    }
}
