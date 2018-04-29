using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utility.Miscellaneous
{
    public static class EmailValidator
    {

        // email syntax or pattern chaking regex
        private const string EMAIL_PATTERN = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
           + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
           + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        // get regex instance 
        static Regex ValidEmailRegex = CreateValidEmailRegex();
        /// <summary>
        /// This function is used to create new instance of regex class
        /// </summary>
        /// <returns>Instance of regex class</returns>
        private static Regex CreateValidEmailRegex()
        {

            return new Regex(EMAIL_PATTERN, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// This function is validate the email string format of pattern and return true if valid else  return false.
        /// </summary>
        /// <param name="emailAddress">(string) email address</param>
        /// <returns>(bool) return pattern valid status result</returns>
        public static bool EmailPatternIsValid(this string emailAddress)
        {
            return ValidEmailRegex.IsMatch(emailAddress);
        }

        /* 
         * ----------------------------------------------------------------------
         This class is tested with following valid/invalid email addresses list:
         ---------------------------------------------------------------------------
         * *************************************************************************  
         *************************************************************************** 
            List<string> list = new List<string>();
            list.Add("email@domain.com");
            list.Add("firstname.lastname@domain.com");
            list.Add("email@subdomain.domain.com");
            list.Add("firstname+lastname@domain.com");
            list.Add("1234567890@domain.com");
            list.Add("email@domain-one.com");
            list.Add("_______@domain.com");
            list.Add("email@domain.name");
            list.Add("email@domain.co.jp");
            list.Add("firstname-lastname@domain.com");
            list.Add("plainaddress");
            list.Add("#@%^%#$@#$@#.com");
            list.Add("@domain.com");
            list.Add("Joe Smith <email@domain.com>");
            list.Add("email.domain.com");
            list.Add("email@domain@domain.com");
            list.Add(".email@domain.com");
            list.Add("email.@domain.com");
            list.Add("email..email@domain.com");
            list.Add("あいうえお@domain.com");
            list.Add("email@domain.com (Joe Smith)");
            list.Add("email@domain");
            list.Add("email@-domain.com");
            list.Add("email@domain.web");
            list.Add("email@111.222.333.44444");
            list.Add("email@domain..com");
         *************************************************************************** 
         **************************************************************************** 
         */

    }
}
