using System.Collections.Generic;
using deprosa.ViewModel;
using deprosa.Web.Data.Model.ViewModel;

namespace deprosaWeb.Model.ViewModel
{
    public class HighlightViewModel
    {
        public CategoryViewModel CategoryViewModel { get; set; } = new CategoryViewModel();

        public  List<SaleListingDTO> HighligthtedSaleListings { get; set; } = new List<SaleListingDTO>();
        
        
    }
}