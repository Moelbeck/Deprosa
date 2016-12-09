using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deprosa.Website.Controllers
{
    public class BaseController : Controller
    {

        public int Sort(string sort = "", string search = "", int? page = null)
        {
            ViewBag.CurrentSort = sort;
            ViewBag.DateSortParm = String.IsNullOrWhiteSpace(sort) ? "created_desc" : "";
            ViewBag.TitleSortParam = sort == "title" ? "title_desc" : "title";
            ViewBag.PriceSortParm = sort == "price" ? "price_desc" : "price";
            int nextpage = 1;
            if (page != null)
            {
                nextpage = (int)page;
            }
            return nextpage;
        }
    }
}