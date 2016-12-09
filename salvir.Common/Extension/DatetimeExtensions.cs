using System;

namespace deprosa.Extension
{
   public static class DatetimeExtensions
    {
        public static String Timestamp( this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }


        public static String MonthTimeStamp(this DateTime value)
        {
            return value.ToString("yyyyMM");
        }
    }
}
