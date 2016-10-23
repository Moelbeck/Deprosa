using depross.Model;
using System.Collections.Generic;
using System.Linq;

namespace depross.Interfaces
{
    public interface ISubCategoryRepository
    {
        IQueryable<SubCategory> GetSubCategoriesByMainID(int maincategory);

        SubCategory GetSubCategory(int categoryid);
        void AddNewSubCategory(SubCategory newCategory);

        void UpdateSubCategory(SubCategory category);
    }
}
