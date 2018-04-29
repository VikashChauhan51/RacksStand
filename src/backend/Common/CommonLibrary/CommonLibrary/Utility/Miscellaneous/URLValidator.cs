
using System.Text.RegularExpressions;

namespace Utility.Miscellaneous
{
    public static class URLValidator
    {
        // URL syntax or pattern chaking regex
        private const string patternURL = "^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$";
        /// <summary>
        /// This method check the url string formate and ture if valid else false.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool URLPatternIsValid(this string url)
        {
            Regex regex = new Regex(patternURL, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return url != null && regex.IsMatch(url);
        }
    }
}
