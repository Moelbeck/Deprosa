using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deprosa.ViewModel;

namespace deprosa.Web.Model.ViewModel
{
    public class MenuViewModel
    {
        public List<MainCategoryDTO> MainCategories { get; set; }

        public MainCategoryDTO SelectedMainCategory { get; set; }

        public List<SubCategoryDTO> SubCategories { get; set; }

        public SubCategoryDTO SelectedSubCategory { get; set; }
    }
}