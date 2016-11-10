using deprosa.ViewModel;
using deprosa.Web.Data.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace deprosa.Web.Model
{
    public class SaleListingCreateViewModel
    {
        public CategoryViewModel Categories { get; set; }

        public SaleListingCreateDTO SaleListing { get; set; }
    }
}