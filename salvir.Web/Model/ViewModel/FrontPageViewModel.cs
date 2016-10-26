using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deprosa.ViewModel;

namespace salvir.Web.Model.ViewModel
{
    public class FrontPageViewModel
    {
        public List<CategoryDTO> MainCategories { get; set; }

        public  List<SaleListingDTO> HighligthtedSaleListings { get; set; }
        
    }
}