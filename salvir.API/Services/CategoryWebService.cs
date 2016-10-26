﻿using AutoMapper;
using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository;
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
        private readonly IMainCategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subcategoryRepository;
        private IProductTypeRepository _productRepository;

        public CategoryWebService()
        {
            BzaleDatabaseContext context = new BzaleDatabaseContext();
            _categoryRepository = new MainCategoryRepository(context);
            _subcategoryRepository = new SubCategoryRepository(context);
            _productRepository = new ProductTypeRepository(context);
        }

        public List<CategoryDTO> GetMainCategories()
        {
            try
            {
                var allcategories = _categoryRepository.GetMainCategories().ToList();
                return allcategories.Select(Mapper.Map<MainCategory, CategoryDTO>).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<CategoryDTO> GetSubCategoriesForMain(int id)
        {
            try
            {
                var subcategories = _subcategoryRepository.GetSubCategoriesByMainID(id).ToList();
                return subcategories.Select(Mapper.Map<SubCategory, CategoryDTO>).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public CategoryDTO GetMainCategory(int id)
        {
            try
            {

                var area = _categoryRepository.GetMainCategory(id);
                return Mapper.Map<MainCategory, CategoryDTO>(area);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<CategoryDTO> GetMainCategoriesBySearchString(string searchstring)
        {
            try
            {
                List<MainCategory> categories = !string.IsNullOrWhiteSpace(searchstring) ? 
                    _categoryRepository.GetMainCategories().Where(e => e.Name.ToLower().Contains(searchstring.ToLower()) || e.Description.ToLower().Contains(searchstring.ToLower())).ToList()
                    : new List<MainCategory>();
                List<CategoryDTO> allsearchedproducts = new List<CategoryDTO>();
                allsearchedproducts.AddRange(categories.Select(Mapper.Map<MainCategory, CategoryDTO>));
                //_log.LogSearch(userid, searchstring);
                return allsearchedproducts;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void CreateMainCategory(CategoryDTO viewmodel)
        {
            try
            {
                MainCategory newcategory = Mapper.Map<CategoryDTO, MainCategory>(viewmodel);
                _categoryRepository.AddMainCategory(newcategory);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void CreateSubCategory(int mainid, CategoryDTO viewmodel)
        {
            try
            {
                SubCategory newcategory = Mapper.Map<CategoryDTO, SubCategory>(viewmodel);
                var main = _categoryRepository.GetMainCategory(mainid);
                newcategory.MainCategory = main;
                _categoryRepository.UpdateMainCategory(main);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
