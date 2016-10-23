

using depross.Common;
using System.ComponentModel.DataAnnotations;

namespace depross.ViewModel
{
    public class ProductTypeDTO
    {
 
        public int ID { get; set; }
 
        [Display(Name="Navn")]
        public string Name { get; set; }
 
        public int SubCategoryID { get; set; }

        public eSalelistingTypes Types { get; set; }

    }
}
