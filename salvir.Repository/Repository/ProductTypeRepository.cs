using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.Interfaces;
using deprosa.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace deprosa.Repository
{
    public class ProductTypeRepository : GenericRepository< ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(BzaleDatabaseContext context) : base(context)
        {

        }
        #region Product
        public ProductType AddProductType(ProductType newproduct)
        {
            //We need to check if the person have verified it somehow
            Add(newproduct);
            Save();
            return newproduct;
        }

        public void UpdateProductType(ProductType updatedProduct)
        {
            Update(updatedProduct);
            Save();
        }

        public bool IsProductInDatabase(ProductType newproduct)
        {
            return GetSingle(e => e.Name.ToLower() == newproduct.Name.ToLower())!=null;
        }


        public ProductType GetProductTypeByID(int productid)
        {
            return GetSingle(e => e.ID == productid);
        }

        public IQueryable<ProductType> GetProductTypesForCategory(int categoryid)
        {
            return Get(e => e.Category.ID == categoryid && e.Deleted == null);
        }

        public IQueryable<ProductType> GetProductsForManufacturer(Manufacturer manufacturer)
        {
            return Get(e => e.ID == manufacturer.ID);
        }

        public IQueryable<ProductType> GetAllProductTypesForCategories(List<int> categoryids)
        {
            return null;
        }

        public IQueryable<ProductType> AllProductTypes()
        {
            return Get(e => e.Deleted != null);
        }
        #endregion

    }
}
