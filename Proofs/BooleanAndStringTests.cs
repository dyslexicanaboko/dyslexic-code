using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proofs_UnitTests
{
    [TestClass]
    public class BooleanAndStringTests
    {
        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_null_BooleanTryParse_returns_false()
        {
            bool boolOut = false;

            Assert.AreEqual(false, bool.TryParse(null, out boolOut));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_empty_BooleanTryParse_returns_false()
        {
            bool boolOut = false;

            Assert.AreEqual(false, bool.TryParse(string.Empty, out boolOut));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_white_space_BooleanTryParse_returns_false()
        {
            bool boolOut = false;

            Assert.AreEqual(false, bool.TryParse(" ", out boolOut));
        }

        #region Fix
        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_one_BooleanTryParse_returns_false()
        {
            bool boolOut = false;

            Assert.AreEqual(false, bool.TryParse("1", out boolOut)); //Risk for false positive
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_zero_BooleanTryParse_returns_false()
        {
            bool boolOut = false;

            Assert.AreEqual(false, bool.TryParse("0", out boolOut)); //Risk for false positive
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_True_BooleanTryParse_returns_true()
        {
            bool boolOut = false;

            Assert.AreEqual(true, bool.TryParse("True", out boolOut));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_False_BooleanTryParse_returns_false()
        {
            bool boolOut = false;

            Assert.AreEqual(true, bool.TryParse("False", out boolOut));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_true_BooleanTryParse_returns_true()
        {
            bool boolOut = false;

            Assert.AreEqual(true, bool.TryParse("true", out boolOut));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_false_BooleanTryParse_returns_true()
        {
            bool boolOut = false;

            Assert.AreEqual(true, bool.TryParse("false", out boolOut));
        }
        #endregion

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_null_ConvertToBoolean_returns_false()
        {
            string str = null;

            Assert.AreEqual(false, Convert.ToBoolean(str));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        [ExpectedException(typeof(FormatException))]
        public void String_empty_ConvertToBoolean_throws_FormatException()
        {
           Convert.ToBoolean(string.Empty);
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        [ExpectedException(typeof(FormatException))]
        public void String_white_space_ConvertToBoolean_throws_FormatException()
        {
            Convert.ToBoolean(" ");
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        [ExpectedException(typeof(FormatException))]
        public void String_one_ConvertToBoolean_throws_FormatException()
        {
            Convert.ToBoolean("1");
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        [ExpectedException(typeof(FormatException))]
        public void String_zero_ConvertToBoolean_throws_FormatException()
        {
            Convert.ToBoolean("0");
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_True_ConvertToBoolean_returns_true()
        {
            Assert.AreEqual(true, Convert.ToBoolean("True"));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_False_ConvertToBoolean_returns_false()
        {
            Assert.AreEqual(false, Convert.ToBoolean("False"));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_true_ConvertToBoolean_returns_true()
        {
            Assert.AreEqual(true, Convert.ToBoolean("true"));
        }

        [TestMethod]
        [TestCategory("String to Boolean")]
        public void String_false_ConvertToBoolean_returns_false()
        {
            Assert.AreEqual(false, Convert.ToBoolean("false"));
        }
    }
}
