using deprosa.Common;
using deprosa.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Model.Log
{
    public class LogSaleListing : BaseLog
    {
        public int SaleListingID { get; set; }

        public int MainCategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public eLogSaleListingType LogType{get;set;}
    }
}
