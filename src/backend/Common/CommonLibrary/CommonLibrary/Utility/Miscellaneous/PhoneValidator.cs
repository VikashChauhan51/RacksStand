using Enums.Miscellaneous;
using Utility.Core;

namespace Utility.Miscellaneous
{
    public static class PhoneValidator
    {
        /// <summary>
        /// This function is validate the phne string format of pattern and return true if valid else  return false.
        /// This method also validate the phone string pattern according to country. By default it's validate universe phone string formate.
        /// </summary>
        /// <param name="phone"> (PatternType enum) value.</param>
        /// <param name="patternType"></param>
        /// <returns></returns>
        public static bool PhonePatternIsValid(this string phone, PatternType patternType = PatternType.Universal)
        {
            return  PatternString.GetRegexInstance(patternType).IsMatch(phone);
        }

    }
}
