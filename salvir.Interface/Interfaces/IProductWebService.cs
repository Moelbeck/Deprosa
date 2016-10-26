using deprosa.Model;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Interfaces
{
    public interface IProductWebService
    {
        List<ProductTypeDTO> GetProductTypesForSubCategory(int categoryid);
        ProductTypeDTO GetProdyctType(int typeid);
        List<ProductType> GetAllProductsTypesByString(string searchstring);
    }
}
