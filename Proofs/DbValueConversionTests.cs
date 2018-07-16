using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proofs_UnitTests
{
    [TestClass]
    public class DbValueConversionTests
    {
        private DataRow _dr = null;
        private DataTable _dt = null;
        private IDataReader _idr = null;

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

            _dt = dt;
            _dr = dt.Rows[0];
            _idr = dt.CreateDataReader();
            _idr.Read();
        }

        [TestMethod]
        public void DataRow_string_DBNull_ConvertToString_returns_null()
        {
            Assert.AreEqual(null, DbValueConversion.ConvertToString(_dr, "StringColDBNull"));
        }

        [TestMethod]
        public void DataRow_Int32_DBNull_ConvertToInt32_returns_null()
        {
            Assert.AreEqual(null, DbValueConversion.ConvertToType(_dr, "Int32Col", Convert.ToInt32));
        }

        [TestMethod]
        public void IDataReader_string_DBNull_ConvertToString_returns_null()
        {
            Assert.AreEqual(null, DbValueConversion.ConvertToString(_idr, "StringColDBNull"));
        }

        [TestMethod]
        public void IDataReader_Int32_DBNull_ConvertToInt32_returns_null()
        {
            Assert.AreEqual(null, DbValueConversion.ConvertToType(_idr, "Int32Col", Convert.ToInt32));
        }
    }
}
