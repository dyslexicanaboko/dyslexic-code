using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/* EHH - 03/21/10 - I am going to put any and all useful RegEx that I will or have used into this class.
 * */ 
namespace ServerOps
{
    public class RegExManager
    {
        #region Stock Regular Expression String Patterns and Notes

        /* WARNING:
         * ========================================================================================
         * When Creating a new Regular Expression, MAKE SURE that if you have any escape or
         * special characters, that you insert them into the string using the actual character.
         * Otherwise it will be misinterpreted as it is being processed. For example any \ character
         * will be interpreted as an escape for the next character. If you look below, you can see
         * "\b", this must be written as Utils.GetChar(92) + "b" otherwise it will be read an an
         * escape character.
         * 
         * CHARS:
         * ========================================================================================
         * 34 - "
         * 92 - \
         * 
         * STOCK REGEX:
         * ========================================================================================
         * </*?([A-Z][A-Z0-9]*)\b[^>]*> <-- This will match ANY html or XML tag regardless of how it is formed
         *                                  Examples: <tag />, <tag> </tag>, <tag prop="info" /> etc...
         *                                  
         * </*?([A ][A-Z0-9="/]*)\b[^>]*>  <-- this will match the <a href="..."> starting tag and </a> tags only
         * 
         * <!--([A-Z0-9]*)[^>]*-->  <-- this will match the XML or HTML comment tags - it has a problem though, it 
         *                              will prematurely close if the ">" char is found. I am trying to figure it out.
         * [ \t]+ <-- Match all white space and tabs regardless of the number
         * [ ]+   <-- Match all white space regardless of the number, do not match tabs.
         * */
        public static string ANY_TAG = "</*?([A-Z][A-Z0-9]*)" + Utils.GetChar(92) + "b[^>]*>"; /* Match any HTML/XML starting or ending tag */
        public static string ANCHOR = "</*?([A ][A-Z0-9=" + Utils.GetChar(34) + "/]*)" + Utils.GetChar(92) + "b[^>]*>"; /* Match starting or ending Anchor Tags */
        public static string HTML_XML_COMMENT = "<!--([A-Z0-9]*)[^>]*-->"; //Match all of an HTML or XML comment
        public static string WHITE_SPACE_TABS = "[ " + Utils.GetChar(92) + "t]+"; //Match all white space and tabs regardless of the number
        public static string WHITE_SPACE_ONLY = "[ ]+"; //Match all white space only, leave tabs alone
        #endregion

        private Regex _regex = null;

        private string _strRegex = string.Empty;
        public string RegularExpression
        {
            get { return _strRegex; }
            set { _strRegex = value; }
        }

        private RegexOptions _regexOptions;
        public RegexOptions RegularExpressionOptions
        {
            get { return _regexOptions; }
            set { _regexOptions = value; }
        }

        public RegExManager()
        { 
            _regexOptions = RegexOptions.IgnoreCase;     
        }

        public RegExManager(string regularExpression)
        { 
            _strRegex = regularExpression;
            _regexOptions = RegexOptions.IgnoreCase;
        }

        public RegExManager(string regularExpression, RegexOptions options)
        {
            _strRegex = regularExpression;
            _regexOptions = options;
        }

        public bool Initialize()
        { 
            bool initialized = false;
            
            try
            {
                if(!Utils.IsBlankOrNull(_strRegex))
                {
                    _regex = new Regex(_strRegex, _regexOptions);
                    initialized = true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }

            return initialized;
        }

        public bool ReplaceAllMatches(string input, string replacement, out string output)
        {
            bool success = false;

            try
            {
                output = string.Empty; //First initialize it
                output = _regex.Replace(input, replacement); //Do the replace operation and store the result

                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }

        public string[] GetAllMatches(string input)
        {
            string[] stringArr = null;

            try
            {
                stringArr = _regex.Split(input);
            }
            catch (Exception e)
            {
                throw e;
            }

            return stringArr;
        }

        public static string LeftOfString(string target, string find)
        {
            string strSubString = string.Empty;

            try
            {
                strSubString = target.Substring(0, target.ToLower().IndexOf(find.ToLower()));
            }
            catch (Exception ex)
            {
                throw;
            }

            return strSubString;
        }

        public static string RightOfString(string target, string find)
        {
            int intStartIndex = 0;
            string strSubString = string.Empty;

            try
            {
                intStartIndex = target.ToLower().IndexOf(find.ToLower()) + find.Length;

                strSubString = target.Substring(intStartIndex, target.Length - intStartIndex);
            }
            catch (Exception ex)
            {
                throw;
            }

            return strSubString;
        }
    }
}
