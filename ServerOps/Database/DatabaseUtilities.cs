using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace KawaUtilLibrary.Database
{
    public static class DatabaseUtilities
    {
        /// <summary>
        /// Wrap a raw string in hard quotes and remove any illegal or unsafe database characters
        /// </summary>
        /// <param name="input">The string to prepare</param>
        /// <returns>A prepared string for a query</returns>
        public static string AddString(this string input)
        {
            if (input == null)
                input = string.Empty;

            return "'" + SanitizeString(input) + "'";
        }

        /// <summary>
        /// Remove any illegal or unsafe characters for use in a query
        /// </summary>
        /// <param name="input">The string to prepare</param>
        /// <returns>A sanitized string</returns>
        public static string SanitizeString(this string input)
        {
            return input.TrimEnd('\\').Replace("\"", "").Replace("'", "''");            
        }

        /// <summary>
        /// Remove any illegal or unsafe characters for use in a search string query
        /// </summary>
        /// <param name="input">The string to prepare</param>
        /// <returns>A sanitized string search string</returns>
        public static string AddSearchString(this string input)
        {
            return SanitizeString(input);
        }

        public static StringBuilder AddSearchString(this StringBuilder target, string column, string input)
        {
            //There are special characters that need to be wrapped appropriately when they are going to show up in a LIKE clause
            //http://msdn.microsoft.com/en-us/library/ms179859.aspx
            //TODO: I am only handling the underscore case right now, but %, [, ] and ^ need to be handled
            return target.Append(column).Append(" LIKE '%").Append(input.SanitizeString().Replace("_", "[_]")).Append("%' ");
        }

        /// <summary>
        /// Sanitize a list of strings using the AddString() method and return it as a CSV string.
        /// This will not affect the original list.
        /// </summary>
        /// <param name="input">The list of strings to prepare</param>
        /// <returns>A CSV of sanitized strings</returns>
        public static string AddString(this List<string> input)
        {
            //Make a copy of the list being passed in so the original elements are preserved
            List<string> lst = new List<string>(input);

            for (int i = 0; i < lst.Count; i++)
                lst[i] = AddString(lst[i]);

            return string.Join(",", lst.ToArray());
        }

        /// <summary>
        /// Wrap a DateTime object in hard quotes with the Universal sortable date/time pattern.
        /// </summary>
        /// <param name="input">The DateTime object to prepare</param>
        /// <returns>A sanitized DateTime string in Universal sortable date/time pattern</returns>
        public static string AddString(this DateTime input)
        {
            return "'" + input.ToString("u") + "'";
        }
    }
}
