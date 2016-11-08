using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Common.RequestWrappers
{
    public class HighlightSalelistingRequest
    {
        public List<SaleListingDTO> HighlightedSalelistings { get; set; }
        public List<SubCategoryDTO> SubCategories { get; set; }
        public SubCategoryDTO SelectedSubCategory { get; set; }
        public MainCategoryDTO SelectedMainCategory { get; set; }
    }
}
