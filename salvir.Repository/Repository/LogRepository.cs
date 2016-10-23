//using bzale.Model.Base;
//using bzale.Model.Log;
//using bzale.Repository.Abstract;
//using bzale.Repository.DatabaseContext;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace bzale.Repository
//{
//    public class LogRepository : GenericRepository< BaseLog>
//    {
//        public LogRepository(BzaleDatabaseContext context) : base(context)
//        {

//        }
//        #region Search
//        public void LogSearch(LogUserSearch search)
//        {
//            Task.Factory.StartNew(() =>
//            {
//                Add(search);
//                SaveAsync();
//            });
//        }
//        #endregion

//        #region Salelisting
//        public void LogSaleListing(LogUserSaleListing salelisting)
//        {
//            Task.Factory.StartNew(() =>
//            {
//                Add(salelisting);
//                SaveAsync();
//            });
//        }
//        #endregion

//        public void LogUserLogin(LogUserLogin login)
//        {
//            Task.Factory.StartNew(() =>
//            {
//                Add(login);
//                SaveAsync();

//            });
//        }
//    }
//}
