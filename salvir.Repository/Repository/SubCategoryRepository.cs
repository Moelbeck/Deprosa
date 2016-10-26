using deprosa.Interfaces;
using deprosa.Model;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.WebService
{
    public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRepository
    {

        public SubCategoryRepository(BzaleDatabaseContext context) : base(context)
        {

        }
        public IQueryable<SubCategory> GetSubCategoriesByMainID(int maincategory)
        {
            return Get(e => e.MainCategory.ID == maincategory && e.Deleted == null);
        }

        public SubCategory GetSubCategory(int categoryid)
        {
            return GetSingle(e => e.ID == categoryid && e.Deleted == null);
        }
        public void AddNewSubCategory(SubCategory newCategory)
        {
            Add(newCategory);
            Save();
        }

        public void UpdateSubCategory(SubCategory category)
        {
            Edit(category);
            Save();
        }
    }

}
