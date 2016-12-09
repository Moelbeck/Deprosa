using deprosa.ViewModel;
using System.Collections.Generic;

namespace deprosa.Common
{
   public class CategoryStructureRequest
    {
        public List<MainCategoryDTO> MainCategories { get; set; } = new List<MainCategoryDTO>();

        public List<SubCategoryDTO> SubCategories { get; set; } = new List<SubCategoryDTO>();

        public List<ProductTypeDTO> ProductTypes { get; set; } = new List<ProductTypeDTO>();
    }
}
