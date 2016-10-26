using deprosa.Model;
using System.Collections.Generic;
using System.Linq;

namespace deprosa.Interfaces
{
    public interface IMainCategoryRepository
    {

        IQueryable<MainCategory> GetMainCategories();
        IQueryable<MainCategory> GetCategoriesById(List<int> ids);

        MainCategory GetMainCategory(int categoryid);
        void AddMainCategory(MainCategory newCategory);

        void UpdateMainCategory(MainCategory category);

        void RemoveMainCategory(int id);
    }
}
