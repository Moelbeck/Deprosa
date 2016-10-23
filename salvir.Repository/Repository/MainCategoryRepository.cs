using System.Collections.Generic;
using depross.Repository.DatabaseContext;
using depross.Repository.Abstract;
using System.Linq;
using depross.Model;
using depross.Interfaces;
using System;

namespace depross.Repository
{
    public class MainCategoryRepository : GenericRepository<MainCategory>, IMainCategoryRepository
    {

        public MainCategoryRepository(BzaleDatabaseContext context) : base(context)
        {
        }
        public IQueryable<MainCategory> GetMainCategories()
        {
            return Get(e => e.Deleted == null);
        }
        public IQueryable<MainCategory> GetCategoriesById(List<int> ids)
        {
            return Get(e => ids.Contains(e.ID) && e.Deleted == null);
        }

        public MainCategory GetMainCategory(int categoryid)
        {
            return GetSingle(e => e.ID == categoryid && e.Deleted == null);
        }
        public void AddMainCategory(MainCategory newCategory)
        {
            Add(newCategory);
            Save();
        }

        public void UpdateMainCategory(MainCategory category)
        {
            Edit(category);
            Save();
        }
        public void RemoveMainCategory(int id)
        {
            Delete(id);
            Save();
        }
    }
}
