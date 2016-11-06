using deprosa.Model.Base;
using deprosa.Model.Log;
using deprosa.Model.Model.Log;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deprosa.API.Services.Internal_Services
{
    public class LogService
    {
        public GenericRepository<LogCategory> _categorylogrepo;
        public GenericRepository<LogLogin> _loginlogrepo;
        public GenericRepository<LogSaleListing> _salelistinglogrepo;
        public GenericRepository<LogSearch> _searchlogrepo;
        public LogService()
        {
            var context = new BzaleDatabaseContext();
            _categorylogrepo = new GenericRepository<LogCategory>(context);
            _loginlogrepo = new GenericRepository<LogLogin>(context);
            _salelistinglogrepo = new GenericRepository<LogSaleListing>(context);
            _searchlogrepo = new GenericRepository<LogSearch>(context);
        }
        public void LogLogin(LogLogin login)
        {
            _loginlogrepo.Add(login);
        }
        /// <summary>
        /// Logs when a user looks at a salelisting, updating one or deleting it.
        /// </summary>
        public void LogSaleListing(LogSaleListing salelisting)
        {
            _salelistinglogrepo.Add(salelisting);
        }
        /// <summary>
        /// Logs when a user clicks on a category - both sub and main.
        /// </summary>
        public void LogCategory(LogCategory category)
        {
            _categorylogrepo.Add(category);
        }
        /// <summary>
        /// Logs when a user searches for something.
        /// </summary>
        public void LogSearch(LogSearch search)
        {
            _searchlogrepo.Add(search);
        }

        public List<LogCategory> GetPopularMainCategories()
        {
            var mostpopularkeys = _categorylogrepo
                .Get(e=>e.MainCateogryId !=0 && e.SubCategoryId == 0)
                .GroupBy(g=>g.MainCateogryId)
                .OrderByDescending(gp=>gp.Count())
                .Take(3)
                .Select(e=>e.Key).ToList();
            var mostpopularcategories = _categorylogrepo.Get(e => mostpopularkeys.Contains(e.MainCateogryId)).ToList();
            return mostpopularcategories;
        }
        public List<LogCategory> GetPopularSubCategories()
        {
            var mostpopularkeys = _categorylogrepo
                .Get(e => e.MainCateogryId == 0 && e.SubCategoryId != 0)
                .GroupBy(g => g.MainCateogryId)
                .OrderByDescending(gp => gp.Count())
                .Take(3)
                .Select(e => e.Key).ToList();
            var mostpopularcategories = _categorylogrepo.Get(e => mostpopularkeys.Contains(e.MainCateogryId)).ToList();
            return mostpopularcategories;
        }
        public List<LogCategory> GetPopularSubCategoriesForUser(int userid)
        {
            return null;
        }

        public List<LogSaleListing> GetPopularSalelistingsForMain(int main)
        {
            return null;
        }

        public List<LogSaleListing> GetPopularSalelistingsForSub(int sub)
        {
            return null;
        }
        public List<LogSaleListing> GetPopularSalelistingsForUser(int userId)
        {
            return null;
        }
    }
}