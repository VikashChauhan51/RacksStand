using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RacksStand.WebAPI
{
    public class AppSettingManager
    {

        /// <summary>
        /// Salt for Encryption/Decryption.
        /// </summary>
        public static string Salt
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Salt"]))
                    throw new ArgumentNullException("AppSettingManager.Salt");
                return ConfigurationManager.AppSettings["Salt"];
            }
        }

        /// <summary>
        /// Salt for string Encryption/Decryption.
        /// </summary>
        public static string Password
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Password"]))
                    throw new ArgumentNullException("AppSettingManager.Password");
                return ConfigurationManager.AppSettings["Password"];
            }
        }
       
        /// <summary>
        /// get client application base URL.
        /// </summary>
        public static string BaseURL
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["BaseURL"]))
                    throw new ArgumentNullException("AppSettingManager.BaseURL");
                return ConfigurationManager.AppSettings["BaseURL"];
            }
        }
    }
}