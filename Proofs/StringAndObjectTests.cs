using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proofs_UnitTests
{
    [TestClass]
    public class StringAndObjectTests
    {
        [TestMethod]
        [TestCategory("Object to String")]
        public void Object_null_ConvertToString_returns_empty_string()
        {
            object objNull = null;

            Assert.AreEqual(string.Empty, Convert.ToString(objNull));
        }

        [TestMethod]
        [TestCategory("String to String")]
        public void String_null_ConvertToString_returns_null()
        {
            string strNull = null;

            Assert.AreEqual(null, Convert.ToString(strNull));
        }

        [TestMethod]
        [TestCategory("String to String")]
        public void String_null_as_object_ConvertToString_returns_empty_string()
        {
            string strNull = null;
            
            Assert.AreEqual(string.Empty, Convert.ToString(strNull as object));
        }
    }
}
