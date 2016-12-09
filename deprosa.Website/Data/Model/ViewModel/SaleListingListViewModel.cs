using deprosa.ViewModel;
using deprosa.Web.Data.Model.ViewModel;
using PagedList;

namespace deprosa.Website.Data.Model.ViewModel
{
    public class SaleListingListViewModel
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public IPagedList<SaleListingDTO> Salelistings { get; set; }
    }
}