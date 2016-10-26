using deprosa.Common;
using deprosa.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprosa.Model.Log
{
    public class LogUserSaleListing : BaseLog
    {
        public int SaleListingID { get; set; }

        public eLogSaleListingType LogType{get;set;}
    }
}
