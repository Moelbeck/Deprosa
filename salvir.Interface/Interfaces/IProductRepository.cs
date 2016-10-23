using depross.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace depross.Interfaces

{
    public interface IProductTypeRepository
    {
        ProductType AddProductType(ProductType newproduct);

        void UpdateProductType(ProductType updatedProduct);

        ProductType GetProductTypeByID(int productid);
        IQueryable<ProductType> AllProductTypes();

        IQueryable<ProductType> GetProductTypesForCategory(int categoryid);

        IQueryable<ProductType> GetProductsForManufacturer(Manufacturer manufacturer);

        bool IsProductInDatabase(ProductType newproduct);

        IQueryable<ProductType> GetAllProductTypesForCategories(List<int> categoryids);
    }
}
