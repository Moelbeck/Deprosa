using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deprosa.ViewModel;
using PagedList;

namespace deprosa.Web.Model
{
    public class SaleListingListViewModel
    {

        public IPagedList<SaleListingDTO> Salelistings { get; set; }
    }
}