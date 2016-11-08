using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deprosa.ViewModel;
using deprosa.Web.Model.ViewModel;

namespace deprosaWeb.Model.ViewModel
{
    public class HighlightViewModel
    {
        public MenuViewModel MenuViewModel { get; set; } = new MenuViewModel();

        public  List<SaleListingDTO> HighligthtedSaleListings { get; set; } = new List<SaleListingDTO>();
        
    }
}