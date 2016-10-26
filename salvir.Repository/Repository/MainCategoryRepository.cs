using System.Collections.Generic;
using deprosa.Repository.DatabaseContext;
using deprosa.Repository.Abstract;
using System.Linq;
using deprosa.Model;
using deprosa.Interfaces;
using System;

namespace deprosa.Repository
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
            Update(category);
            Save();
        }
        public void RemoveMainCategory(int id)
        {
            Delete(id);
            Save();
        }
    }
}
