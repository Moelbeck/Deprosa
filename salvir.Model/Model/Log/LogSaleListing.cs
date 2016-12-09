using deprosa.Common;
using deprosa.Model.Base;

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
