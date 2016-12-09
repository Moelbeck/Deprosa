using System;

namespace Common.Extension
{
    public static class EnumExtension
    {

        public static string EnumToString<T>(this T enumtype) where T : struct, IConvertible
        {
            string value = Enum.GetName(typeof(T), enumtype).ToString();

            return value;
        }

        public static T StringToEnum<T>(this string enumstring) where T: struct, IConvertible
        {
            T foundenum = (T) Enum.Parse(typeof(T), enumstring);
            return foundenum;
        }
    }
}
