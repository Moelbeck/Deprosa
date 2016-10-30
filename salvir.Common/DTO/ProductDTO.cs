

using deprosa.Common;
using System.ComponentModel.DataAnnotations;

namespace deprosa.ViewModel
{
    public class ProductTypeDTO
    {
 
        public int ID { get; set; }
 
        [Display(Name="Navn")]
        public string Name { get; set; }
 
        public int SubCategoryID { get; set; }
        
        public SubCategoryDTO SubCategory { get; set; }

        public eSalelistingTypes Types { get; set; }

    }
}
