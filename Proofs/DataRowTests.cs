using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Proofs_UnitTests
{
    [TestClass]
    public class DataRowTests
    {
        private DataRow _dr = null;

        [TestInitialize]
        public void Initialize()
        { 
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("StringColDBNull", typeof(string)));
            dt.Columns.Add(new DataColumn("StringColNull", typeof(string)));
            dt.Columns.Add(new DataColumn("Int32Col", typeof(int)));
            dt.Columns.Add(new DataColumn("BooleanCol", typeof(bool)));

            DataRow dr = dt.NewRow();

            dr["StringColDBNull"] = DBNull.Value;
            dr["StringColNull"] = null;
            dr["Int32Col"] = DBNull.Value;
            dr["BooleanCol"] = DBNull.Value;

            dt.Rows.Add(dr);

            _dr = dt.Rows[0];
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_DBNull_ToString_returns_empty_string()
        {
            Assert.AreEqual(string.Empty, _dr["StringColDBNull"].ToString());
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_DBNull_ConvertToString_returns_empty_string()
        {
            Assert.AreEqual(string.Empty, Convert.ToString(_dr["StringColDBNull"]));
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_null_ToString_returns_empty_string()
        {
            Assert.AreEqual(string.Empty, _dr["StringColNull"].ToString());
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_null_ConvertToString_returns_empty_string()
        {
            Assert.AreEqual(string.Empty, Convert.ToString(_dr["StringColNull"]));
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_null_cast_as_object_ConvertToString_returns_empty_string()
        {
            object obj = _dr["StringColNull"] as object;

            Assert.AreEqual(string.Empty, Convert.ToString(obj));
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_DBNull_cast_as_object_ConvertToString_returns_empty_string()
        {
            object obj = _dr["StringColDBNull"] as object;

            Assert.AreEqual(string.Empty, Convert.ToString(obj));
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_null_cast_as_object_ToString_returns_empty_string()
        {
            object obj = _dr["StringColNull"] as object;

            Assert.AreEqual(string.Empty, obj.ToString());
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_DBNull_cast_as_object_ToString_returns_empty_string()
        {
            object obj = _dr["StringColDBNull"] as object;

            Assert.AreEqual(string.Empty, obj.ToString());
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_null_cast_as_string_ConvertToString_returns_null()
        {
            string obj = _dr["StringColNull"] as string;

            Assert.AreEqual(null, Convert.ToString(obj));
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_DBNull_cast_as_string_ConvertToString_returns_null()
        {
            string obj = _dr["StringColDBNull"] as string;

            Assert.AreEqual(null, Convert.ToString(obj));
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        [ExpectedException(typeof(NullReferenceException))]
        public void DataRow_string_type_as_null_cast_as_string_ToString_throws_NullReferenceException()
        {
            string obj = _dr["StringColNull"] as string;

            obj.ToString();
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        [ExpectedException(typeof(NullReferenceException))]
        public void DataRow_string_type_as_DBNull_cast_as_string_ToString_throws_NullReferenceException()
        {
            string obj = _dr["StringColDBNull"] as string;

            Assert.AreEqual(string.Empty, obj.ToString());
        }

        [TestMethod]
        [TestCategory("DataRow to string")]
        public void DataRow_string_type_as_null_ConvertToString_is_empty_string()
        {
            string obj = Convert.ToString(_dr["StringColNull"]);

            Assert.AreEqual(string.Empty, obj);
        }

        [TestMethod]
        [TestCategory("DataRow to Int32")]
        [ExpectedException(typeof(InvalidCastException))]
        public void DataRow_Int32_type_as_DBNull_ConvertToInt32_throws_InvalidCastException()
        {
            Convert.ToInt32(_dr["Int32Col"]);
        }
    }
}
