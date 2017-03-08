using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RacksStand.Utility.Miscellaneous
{
    /// <summary>
    /// This class is used to generate random string.
    /// </summary>
    public class RandomGenerator
    {
        /// <summary>
        /// This method is used to generate random string of (n)characters length.
        /// The default length is (10).
        /// </summary>
        /// <param name="passwordLength">string length</param>
        /// <returns>random (n)characters length string.</returns>
        public static string GenerateRandomString(int passwordLength = 10)
        {
            string str = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chArray = new char[passwordLength];
            Random random = new Random();
            for (int index = 0; index < passwordLength; ++index)
                chArray[index] = str[random.Next(0, str.Length)];
            return new string(chArray);
        }
        /// <summary>
        /// Gets a unique hash code string of (n)characters length.
        /// The default length is (32).
        /// </summary>
        /// <param name="length">(int)The length of the key returned.</param>
        /// <returns>(string)unique key returned.</returns>
        public static string GetUniqueKey(int length=32)
        {
            string result = string.Empty;

            while (result.Length < length)
            {
                // Get the GUID.
                result += Guid.NewGuid().ToString().GetHashCode().ToString("x");
            }

            // Make sure length is valid.
            if (length <= 0 || length > result.Length)
                throw new ArgumentException("Length must be between 1 and " + result.Length);

            // Return the first length bytes.
            return result.Substring(0, length);
        }
    }
}
