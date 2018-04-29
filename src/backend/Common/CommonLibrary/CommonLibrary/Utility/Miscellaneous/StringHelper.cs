
using System.Text.RegularExpressions;
 

namespace Utility.Miscellaneous
{
    public static class StringHelper
    {
        public static string TryTrim(this string value)
        {
            return value == null ? value : value.Trim();
        }

        public static string TryTrimAll(this string value)
        {
            return value == null ? value : Regex.Replace(value, "^\\s+|\\s+$", string.Empty);
        }

        public static string TryToUpper(this string value)
        {
            return value == null ? value : value.ToUpper();
        }

        public static string TryToLower(this string value)
        {
            return value == null ? value : value.ToLower();
        }

        public static string TryTrimEnd(this string value)
        {
            return value == null ? value : value.TrimEnd();
        }

        public static string TryTrimStart(this string value)
        {
            return value == null ? value : value.TrimStart();
        }

        public static string InitCap(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return value;
            char[] chArray = value.ToCharArray();
            chArray[0] = char.ToUpper(chArray[0]);
            return new string(chArray);
        }
    }
}
