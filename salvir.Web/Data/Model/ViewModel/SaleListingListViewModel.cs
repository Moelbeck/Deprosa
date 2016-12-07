using deprosa.ViewModel;
using PagedList;

namespace deprosa.Web.Data.Model.ViewModel
{
    public class SaleListingListViewModel
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public IPagedList<SaleListingDTO> Salelistings { get; set; }
    }
}