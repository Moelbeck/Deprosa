﻿using AutoMapper;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using deprosa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CategoryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CategoryService.svc or CategoryService.svc.cs at the Solution Explorer and start debugging.
    public class CategoryWebService : ICategoryWebService
    {
        //private readonly IMainCategoryRepository _categoryRepository;
        //private readonly ISubCategoryRepository _subcategoryRepository;
        private readonly GenericRepository<MainCategory> _maincategory;
        private readonly GenericRepository<SubCategory> _subcategory;
        private IProductTypeRepository _productRepository;

        public CategoryWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            //_categoryRepository = new MainCategoryRepository(context);
            //_subcategoryRepository = new SubCategoryRepository(context);
            _productRepository = new ProductTypeRepository(context);
            _maincategory = new GenericRepository<MainCategory>(context);
            _subcategory = new GenericRepository<SubCategory>(context);
        }

        public List<MainCategoryDTO> GetMainCategories()
        {
            try
            {
                var allcategories = _maincategory.Get(e=>e.Deleted == null).ToList();
                return allcategories.Select(Mapper.Map<MainCategory, MainCategoryDTO>).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SubCategoryDTO> GetSubCategoriesForMain(int id)
        {
            try
            {
                var subcategories = _subcategory.Get(e=>e.MainCategory.ID == id && e.Deleted == null).ToList();
                return subcategories.Select(Mapper.Map<SubCategory, SubCategoryDTO>).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public MainCategoryDTO GetMainCategory(int id)
        {
            try
            {

                var main = _maincategory.GetSingle(e=>e.ID == id && e.Deleted == null);
                return Mapper.Map<MainCategory, MainCategoryDTO>(main);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<MainCategoryDTO> GetMainCategoriesBySearchString(string searchstring)
        {
            try
            {
                List<MainCategory> categories = !string.IsNullOrWhiteSpace(searchstring) ? 
                    _maincategory.Get(e => e.Name.ToLower().Contains(searchstring.ToLower()) || e.Description.ToLower().Contains(searchstring.ToLower())).ToList()
                    : new List<MainCategory>();
                List<MainCategoryDTO> allsearchedproducts = new List<MainCategoryDTO>();
                allsearchedproducts.AddRange(categories.Select(Mapper.Map<MainCategory, MainCategoryDTO>));
                //_log.LogSearch(userid, searchstring);
                return allsearchedproducts;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CreateMainCategory(MainCategoryDTO viewmodel)
        {
            try
            {
                MainCategory newcategory = Mapper.Map<MainCategoryDTO, MainCategory>(viewmodel);
                _maincategory.Add(newcategory);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void CreateSubCategory(int mainid, SubCategoryDTO viewmodel)
        {
            try
            {
                SubCategory newcategory = Mapper.Map<SubCategoryDTO, SubCategory>(viewmodel);
                var main = _maincategory.GetSingle(e=>e.ID == mainid);
                newcategory.MainCategory = main;
                _subcategory.Add(newcategory);
                _maincategory.Update(main);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
