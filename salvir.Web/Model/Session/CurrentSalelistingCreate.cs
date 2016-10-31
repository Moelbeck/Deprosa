using deprosa.ViewModel;
using deprosa.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deprosa.Web.Model
{
    public static class CurrentSalelisting
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
        public static void SetSalelistingToNew()
        {
            SaleListingViewModel.CurrentSubCategories = SaleListingViewModel.SubCategories;
            SaleListingViewModel.CurrentProductTypes = SaleListingViewModel.ProductTypes;
            SaleListingViewModel.SaleListing = new SaleListingDTO();
        }
    }
}