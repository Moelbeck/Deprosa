using bzale.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bzale.Web.Model
{
    public static class CurrentSalelistingCreate
    {
        public static SaleListingCreateViewModel SaleListingViewModel
        {
            get
            {
                SaleListingCreateViewModel model = HttpContext.Current.Session["salelistingcreateviewmodel"] as SaleListingCreateViewModel;
                if (model == null)
                {
                    model = new SaleListingCreateViewModel(); 
                    HttpContext.Current.Session["salelistingcreateviewmodel"] = model;
                }
                return model;
            }
            set
            {
                HttpContext.Current.Session["salelistingcreateviewmodel"] = value;
            }
        }
    }
}