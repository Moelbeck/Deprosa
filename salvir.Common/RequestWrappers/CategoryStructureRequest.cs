using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Common
{
   public class CategoryStructureRequest
    {
        public List<MainCategoryDTO> MainCategories { get; set; } = new List<MainCategoryDTO>();

        public List<SubCategoryDTO> SubCategories { get; set; } = new List<SubCategoryDTO>();

        public List<ProductTypeDTO> ProductTypes { get; set; } = new List<ProductTypeDTO>();
    }
}
