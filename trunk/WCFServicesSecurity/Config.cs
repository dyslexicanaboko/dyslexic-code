using System;
using System.Configuration;

namespace WCFServicesSecurity
{
    public static class Config
    {
        public static string HeaderName { get { return GetRawConfigValue("HeaderName"); } }
        public static string HeaderNamespace { get { return GetRawConfigValue("HeaderNamespace"); } }
        public static string AuthorizationTokenName { get { return GetRawConfigValue("AuthorizationTokenName"); } }
        public static string AuthorizationTokenValue { get { return GetRawConfigValue("AuthorizationTokenValue"); } }

        /// <summary>
        /// Retrieves value from ConfigurationManager.AppSettings.
        /// Throws exception if the value returned is null or whitespace.
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns>Value</returns>
        private static string GetRawConfigValue(string keyName)
        {
            string strValue = ConfigurationManager.AppSettings[keyName];

            //Check if the value returned is null or whitespace, in other words invalid
            if (String.IsNullOrWhiteSpace(strValue))
                throw new Exception(string.Format("The key [{0}] was not found or its value was blank. Please add this entry --> <add key=\"{0}\" value=\"\" />", keyName));

            return strValue;
        }
    }
}
