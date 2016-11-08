using deprosa.Model.Base;
using deprosa.Model.Log;
using deprosa.Repository.Abstract;
using deprosa.Repository.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using deprosa.Common;

namespace deprosa.service
{
    public class LogService
    {
        private static int NoOfElements =5;
        private readonly GenericRepository<LogCategory> _categorylogrepo;
        private readonly GenericRepository<LogLogin> _loginlogrepo;
        private readonly GenericRepository<LogSaleListing> _salelistinglogrepo;
        private readonly GenericRepository<LogSearch> _searchlogrepo;
        public LogService()
        {
            var context = new BzaleDatabaseContext();
            _categorylogrepo = new GenericRepository<LogCategory>(context);
            _loginlogrepo = new GenericRepository<LogLogin>(context);
            _salelistinglogrepo = new GenericRepository<LogSaleListing>(context);
            _searchlogrepo = new GenericRepository<LogSearch>(context);
        }
        public void LogLogin(int userid, eLoginType type)
        {
            LogLogin login = new LogLogin { UserID = userid, Type = type};
            _loginlogrepo.Add(login);
        }
        /// <summary>
        /// Logs when a user looks at a salelisting, updating one or deleting it.
        /// </summary>
        public void LogSaleListing(int userid, eLogSaleListingType type, int mainid, int subid, int saleid)
        {
            LogSaleListing saleListing = new LogSaleListing
            {
                UserID = userid,
                LogType = type,
                MainCategoryId = mainid,
                SubCategoryId = subid,
                SaleListingID = saleid
            };
            _salelistinglogrepo.Add(saleListing);
        }
        /// <summary>
        /// Logs when a user clicks on a category - both sub and main.
        /// </summary>
        public void LogCategory(int userid, int mainid, int subid)
        {
            LogCategory category = new LogCategory
            {
                UserID = userid,
                MainCategoryId = mainid, SubCategoryId = subid
            };
            _categorylogrepo.Add(category);
        }
        /// <summary>
        /// Logs when a user searches for something.
        /// </summary>
        public void LogSearch(int userid, string searchstring)
        {
            LogSearch search = new LogSearch
            {
                UserID = userid, SearchString = searchstring
            };
            _searchlogrepo.Add(search);
        }

        public List<int> GetPopularMainCategories()
        {
            return  _categorylogrepo
                .Get(e=>e.MainCategoryId !=0 && e.SubCategoryId == 0)
                .GroupBy(g=>g.MainCategoryId)
                .OrderByDescending(gp=>gp.Count())
                .Take(NoOfElements)
                .Select(e=>e.Key).ToList();
        }
        public List<int> GetPopularSubCategories()
        {
                return _categorylogrepo
                .Get(e => e.MainCategoryId == 0 && e.SubCategoryId != 0)
                .GroupBy(g => g.SubCategoryId)
                .OrderByDescending(gp => gp.Count())
                .Take(NoOfElements)
                .Select(e => e.Key).ToList();
        }
        public List<int> GetPopularSubCategoriesForUser(int userid)
        {
            return _categorylogrepo
                .Get(e => (e.MainCategoryId == 0 && e.SubCategoryId != 0) && e.UserID == userid)
                .GroupBy(g => g.SubCategoryId)
                .OrderByDescending(gp => gp.Count())
                .Take(NoOfElements)
                .Select(e => e.Key).ToList();
        }

        public List<int> GetPopularSalelistingsForMain(int main)
        {
                return _salelistinglogrepo
                .Get(e => e.MainCategoryId == main && e.LogType == eLogSaleListingType.Search)
                .GroupBy(g => g.SaleListingID)
                .OrderByDescending(gp => gp.Count())
                .Take(NoOfElements)
                .Select(e => e.Key).ToList();
        }

        public List<int> GetPopularSalelistingsForSub(int sub)
        {
                return _salelistinglogrepo
                .Get(e => e.SubCategoryId == sub && e.LogType == eLogSaleListingType.Search)
                .GroupBy(g => g.SaleListingID)
                .OrderByDescending(gp => gp.Count())
                .Take(3)
                .Select(e => e.Key).ToList();
        }
        public List<int> GetPopularSalelistingsForUser(int userId)
        {
                return _salelistinglogrepo
                .Get(e => e.UserID == userId && e.LogType == eLogSaleListingType.Search)
                .GroupBy(g => g.SaleListingID)
                .OrderByDescending(gp => gp.Count())
                .Take(3)
                .Select(e => e.Key).ToList();
        }
    }
}