using AutoMapper;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace deprosa.WebApi.Services
{
    public class ProductTypeApiService : IProductWebService
    {
        private GenericRepository<ProductType> _productRepository;

        public ProductTypeApiService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _productRepository = new GenericRepository<ProductType>(context);
        }

        public List<ProductTypeDTO> GetProductTypesForSubCategory(int categoryid)
        {
            var producttypes = _productRepository.Get(e=>e.Category.ID == categoryid).ToList();
            return producttypes.Select(Mapper.Map<ProductType, ProductTypeDTO>).ToList();
        }

        public ProductTypeDTO GetProdyctType(int typeid)
        {
            var producttype = _productRepository.GetSingle(e=>e.ID == typeid);
            return Mapper.Map<ProductType, ProductTypeDTO>(producttype);
        }
        public List<ProductType> GetAllProductsTypesByString(string searchstring)
        {
            var Products = _productRepository.Get(e => e.Name.ToLower().Contains(searchstring)
            || e.Category.Name.ToLower().Contains(searchstring.ToLower())
            || e.Category.Description.ToLower().Contains(searchstring.ToLower())).OrderBy(e => e.ID).ToList();
            return Products.GroupBy(x => x.ID).Select(y => y.First()).ToList();
        }

        public List<ProductTypeDTO> GetAllProductTypes()
        {
            var producttypes = _productRepository.Get(e =>e.Deleted == null).Include(e=>e.Category).Include(e=>e.Category.MainCategory).Select(Mapper.Map<ProductType,ProductTypeDTO>).ToList();
            return producttypes;
        }
    }
}