using System;
using System.Collections.Generic;
using System.Data;

namespace ServerOps
{
    public static class Conversions
    {
        public static string ConvertToString(this DataRow item, string columnName)
        {
            return ConvertToString(item[columnName]);
        }

        /// <summary>
        /// Since Convert.ToString(...) will return null if a string is null,
        /// this method will ensure that blank is returned if a string is null.
        /// </summary>
        /// <param name="item">The object to be converted, regardless if it is null</param>
        /// <returns>object as string or blank</returns>
        public static string ConvertToString(this object item)
        {
            return item == null ? string.Empty : Convert.ToString(item);
        }

        public static string ConvertToStringAndTrim(this DataRow item, string columnName)
        {
            return ConvertToStringAndTrim(item[columnName]);
        }

        public static string ConvertToStringAndTrim(this object item)
        {
            return item.ConvertToString().Trim();
        }

        public static bool ConvertToBoolean(this DataRow item, string columnName)
        {
            return ConvertToBoolean(item[columnName]);
        }

        public static bool ConvertToBoolean(this object item)
        {
            return ConvertToBoolean(Convert.ToString(item));
        }

        public static bool ConvertToBoolean(this string item)
        {
            bool converted = false;

            //Make sure it isn't null or empty, if so FALSE
            if (!string.IsNullOrEmpty(item))
            {
                //If it isn't null or empty, go lower case and trim white space
                item = item.ToLower().Trim();

                //Try converting it normally first, this will only work for
                //the string representation of a bool which is "true" or "false"
                if (!bool.TryParse(item, out converted))
                {
                    //If the previous failed this is the last chance
                    //Check if this is a string representation of a bit
                    //"1" = true, everything else is false!
                    if (item == "1")
                        converted = true;
                }
            }

            return converted;
        }

        public static int ConvertToInt32(this DataRow item, string columnName)
        {
            return ConvertToInt32(item[columnName]);
        }

        public static int ConvertToInt32(this object item)
        {
            return ConvertToInt32(Convert.ToString(item));
        }

        public static int ConvertToInt32(this string item)
        {
            int converted = -1;

            int.TryParse(item, out converted);

            return converted;
        }

        public static decimal ConvertToDecimal(this DataRow item, string columnName)
        {
            return ConvertToDecimal(item[columnName]);
        }

        public static decimal ConvertToDecimal(this object item)
        {
            return ConvertToDecimal(Convert.ToString(item));
        }

        public static decimal ConvertToDecimal(this string item)
        {
            decimal converted = -1M;

            decimal.TryParse(item, out converted);

            return converted;
        }

        public static double ConvertToDouble(this DataRow item, string columnName)
        {
            return ConvertToDouble(item[columnName]);
        }

        public static double ConvertToDouble(this object item)
        {
            return ConvertToDouble(Convert.ToString(item));
        }

        public static double ConvertToDouble(this string item)
        {
            double converted = -1.0;

            double.TryParse(item, out converted);

            return converted;
        }

        public static DateTime ConvertToDateTime(this DataRow item, string columnName)
        {
            return ConvertToDateTime(item[columnName]);
        }

        public static DateTime ConvertToDateTime(this object item)
        {
            return ConvertToDateTime(Convert.ToString(item));
        }

        public static DateTime ConvertToDateTime(this string item)
        {
            DateTime converted = DateTime.MinValue;

            DateTime.TryParse(item, out converted);

            return converted;
        }

        /// <summary>
        /// Convert a DataRow's value to an Enum. The method used is to first convert the value to an integer
        /// and then convert the integer to an enum.
        /// </summary>
        /// <typeparam name="EnumType">The type of the Enumeration to convert to</typeparam>
        /// <param name="item">DataRow where the potential Enum value lives</param>
        /// <param name="columnName">The column name where the potential Enum value lives</param>
        /// <returns>The converted Enum Value</returns>
        public static EnumType ConvertToEnum<EnumType>(this DataRow item, string columnName)
        {
            return ConvertInt32ToEnum<EnumType>(item.ConvertToInt32(columnName));
        }

        public static T ConvertStringToEnum<T>(this string target)
        {
            return (T)Enum.Parse(typeof(T), target);
        }

        //Made a copy of this method and localized it to avoid making a reference to another assembly for a single method
        public static T ConvertInt32ToEnum<T>(this int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        public static T ConvertInt16ToEnum<T>(this short number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        /// <summary>
        /// Convert a DataTable to a list of a specified type of object using a specific Action.
        /// </summary>
        /// <typeparam name="T">Target object type</typeparam>
        /// <param name="dt">The source DataTable</param>
        /// <param name="method">The action to convert the data row columns to object properties</param>
        /// <returns>List of object type T</returns>
        public static List<T> ToList<T>(this DataTable dt, Action<T, DataRow> method) where T : new()
        {
            T obj = default(T);

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                obj = new T();

                method(obj, dr);

                lst.Add(obj);
            }

            return lst;
        }
    }
}
