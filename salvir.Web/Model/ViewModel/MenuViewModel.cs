using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deprosa.ViewModel;

namespace deprosa.Web.Model.ViewModel
{
    public class MenuViewModel
    {
        public List<CategoryDTO> MainCategories { get; set; }
        public List<CategoryDTO> SubCategories { get; set; }
    }
}