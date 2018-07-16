using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proofs_UnitTests
{
    public static class DbValueConversion
    {
        //The dynamic keyword makes everything ULTRA SLOW
        //public static string ConvertToString(dynamic data, string columnName)
        //{
        //    return data[columnName] == DBNull.Value ? null : Convert.ToString(data[columnName]);
        //}

        //public static Nullable<T> ConvertToType<T>(dynamic data, string columnName, Func<object, T> converter)
        //    where T : struct
        //{
        //    return data[columnName] == DBNull.Value ? null : new Nullable<T>(converter(data[columnName]));
        //}

        public static string ConvertToString(IDataReader data, string columnName)
        {
            return data[columnName] == DBNull.Value ? null : Convert.ToString(data[columnName]);
        }

        public static Nullable<T> ConvertToType<T>(IDataReader data, string columnName, Func<object, T> converter)
            where T : struct
        {
            return data[columnName] == DBNull.Value ? null : new Nullable<T>(converter(data[columnName]));
        }

        public static string ConvertToString(DataRow data, string columnName)
        {
            return data[columnName] == DBNull.Value ? null : Convert.ToString(data[columnName]);
        }

        public static Nullable<T> ConvertToType<T>(DataRow data, string columnName, Func<object, T> converter)
            where T : struct
        {
            return data[columnName] == DBNull.Value ? null : new Nullable<T>(converter(data[columnName]));
        }
    }
}
