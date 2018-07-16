using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proofs_UnitTests
{
    [TestClass]
    public class Int32AndBooleanTests
    {
        private string True;
        private string False;

        [TestInitialize]
        public void Initialize()
        {
            //true.ToString() = "True"
            True = true.ToString();

            //false.ToString() = "False"
            False = false.ToString();
        }

        [TestMethod]
        [TestCategory("Boolean to Int32")]
        public void Boolean_true_as_string_Int32TryParse_returns_false()
        {
            int intOut = 0;

            Assert.AreEqual(false, int.TryParse(True, out intOut));
        }

        [TestMethod]
        [TestCategory("Boolean to Int32")]
        public void Boolean_false_as_string_Int32TryParse_returns_false_can_cause_false_positive()
        {
            int intOut = 0;

            Assert.AreEqual(false, int.TryParse(False, out intOut));

            //intOut is still zero which is a false positive if you aren't careful enough
        }

        [TestMethod]
        [TestCategory("Boolean to Int32")]
        [ExpectedException(typeof(FormatException))]
        public void Boolean_true_as_string_ConvertToInt32_throws_FormatException()
        {
            Convert.ToInt32(True); //This will throw an exception
        }

        [TestMethod]
        [TestCategory("Boolean to Int32")]
        [ExpectedException(typeof(FormatException))]
        public void Boolean_false_as_string_ConvertToInt32_throws_FormatException()
        {
            Convert.ToInt32(False); //This will throw an exception
        }

        [TestMethod]
        [TestCategory("Boolean to Int32")]
        public void Boolean_true_ConvertToInt32_yeilds_one()
        {
            Assert.AreEqual(1, Convert.ToInt32(true));
        }

        [TestMethod]
        [TestCategory("Boolean to Int32")]
        public void Boolean_false_ConvertToInt32_yeilds_zero()
        {
            Assert.AreEqual(0, Convert.ToInt32(false));
        }

        [TestMethod]
        [TestCategory("Int32 to Boolean")]
        public void Int32_one_ConvertToBoolean_yeilds_true()
        {
            Assert.AreEqual(true, Convert.ToBoolean(1));
        }

        [TestMethod]
        [TestCategory("Int32 to Boolean")]
        public void Int32_zero_ConvertToBoolean_yeilds_false()
        {
            //This is the only case that yields false
            Assert.AreEqual(false, Convert.ToBoolean(0));
        }

        [TestMethod]
        [TestCategory("Int32 to Boolean")]
        public void Int32_greater_than_one_ConvertToBoolean_yeilds_true()
        {
            Assert.AreEqual(true, Convert.ToBoolean(2));
        }

        [TestMethod]
        [TestCategory("Int32 to Boolean")]
        public void Int32_less_than_zero_ConvertToBoolean_yeilds_true()
        {
            Assert.AreEqual(true, Convert.ToBoolean(-1));
        }
    }
}
