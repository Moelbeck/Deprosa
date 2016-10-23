using AutoMapper;
using depross.Interfaces;
using depross.Model;
using depross.Repository;
using depross.Repository.DatabaseContext;
using depross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace salvir.WebApi.Services
{
    public class ProductTypeWebService : IProductWebService
    {
        private IProductTypeRepository _productRepository;

        public ProductTypeWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _productRepository = new ProductTypeRepository(context);
        }

        public List<ProductTypeDTO> GetProductTypesForSubCategory(int categoryid)
        {
            var producttypes = _productRepository.GetProductTypesForCategory(categoryid);
            return producttypes.Select(Mapper.Map<ProductType, ProductTypeDTO>).ToList();
        }

        public ProductTypeDTO GetProdyctType(int typeid)
        {
            var producttype = _productRepository.GetProductTypeByID(typeid);
            return Mapper.Map<ProductType, ProductTypeDTO>(producttype);
        }
        public List<ProductType> GetAllProductsTypesByString(string searchstring)
        {
            var Products = _productRepository.AllProductTypes().Where(e => e.Name.ToLower().Contains(searchstring)
            || e.Category.Name.ToLower().Contains(searchstring.ToLower())
            || e.Category.Description.ToLower().Contains(searchstring.ToLower())).OrderBy(e => e.ID)
            //.Skip((page - 1) * size).Take(size)
            .ToList();
            return Products.GroupBy(x => x.ID).Select(y => y.First()).ToList();
        }
    }
}