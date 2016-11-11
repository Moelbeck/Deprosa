using deprosa.ViewModel;
using deprosa.Web.Data.Model.ViewModel;

namespace deprosa.Web.Model
{
    public class SaleListingCreateViewModel
    {
        public SaleListingCreateDTO SaleListing { get; set; }
        public CategoryViewModel CategoryViewModel { get; set; }
    }
}