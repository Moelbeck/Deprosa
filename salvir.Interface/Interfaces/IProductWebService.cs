using depross.Model;
using depross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace depross.Interfaces
{
    public interface IProductWebService
    {
        List<ProductTypeDTO> GetProductTypesForSubCategory(int categoryid);
        ProductTypeDTO GetProdyctType(int typeid);
        List<ProductType> GetAllProductsTypesByString(string searchstring);
    }
}
