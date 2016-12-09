using deprosa.ViewModel;
using System.Collections.Generic;

namespace deprosa.Common.RequestWrappers
{
    public class HighlightSalelistingRequest
    {
        public List<SaleListingDTO> HighlightedSalelistings { get; set; }
        public CategoryStructureRequest CategoryStructure { get; set; }
    }
}
