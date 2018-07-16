using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Proofs_UnitTests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void AppKeyWithNoValue()
        {
            Assert.AreEqual(string.Empty, GetConfigValue("keyWithBlank"));
        }

        [TestMethod]
        public void AppKeyWithWhiteSpace()
        {
            Assert.AreEqual(true, string.IsNullOrWhiteSpace(GetConfigValue("keyWithWhiteSpace")));
        }

        [TestMethod]
        public void AppKeyThatDoesNotExistInConfigFile()
        {
            Assert.AreEqual(true, string.IsNullOrWhiteSpace(GetConfigValue("keyThatDoesNotExist")));
        }

        public string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string GetConString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
