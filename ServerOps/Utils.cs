using System;
using System.IO;
using System.Collections;
using System.Net;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ServerOps
{
    /* EHH - 12/06/09 - I created this class with the intentions of it being responsible soley for
     * global functions that will be used repeatedly. Any utility like function, I would have liked
     * to categorize the functions in different classes, but for now they will all reside here.
     */
    public static class Utils
    {
        #region Commonly Used String Constants 
        
        public static string CRLF = "\r\n"; //Line Feed and Carraige Return
        public static string CR   = "\n";   //Carraige Return
        public static string LF   = "\r";   //Line Feed
        public static string yyyyMMdd = "yyyyMMdd";
        public static string MMddyy = "MMddyy";
        public static string DBDateFormat = "yyyy-MM-dd";
        #endregion

        public static string db_FormatString(object inputData)
        {
            return "'" + db_SanitizeInput(inputData.ToString()) + "'";
        }

        //This function will remove any bad characters, trailing spaces etc... and 
        //finally wrap a string in hard quotes.
        public static string db_FormatString(string inputData)
        {
            return "'" + db_SanitizeInput(inputData) + "'";
        }

        public static string db_FormatString(DateTime inputData)
        {
            return db_FormatString(inputData.ToString());
        }

        public static string db_FormatString(int inputData)
        {
            return db_FormatString(inputData.ToString());
        }

        //I need to do the recommended essentials here as usual.
        public static string db_SanitizeInput(string inputData)
        {
            //Make sure to replace any inner hard quotes with escape characters
            return inputData.Trim().Replace("'", "''"); //Not complete
        }

        //Take any date and format it properly: yyyy-mm-dd
        public static string db_FormatDate(string inputDateString)
        {
            return db_FormatDate(DateTime.Parse(inputDateString));
        }

        //Takes a date object and formats it appropriately and returns a string
        public static string db_FormatDate(DateTime inputDate)
        {
            return inputDate.ToString(DBDateFormat);
        }

        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;
            long numBytes = 0;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            
            numBytes = fInfo.Length;

            //Open FileStream to read file
            using (FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read))
            {
                //Use BinaryReader to read file stream into byte array.
                using (BinaryReader br = new BinaryReader(fStream))
                {
                    //When you use BinaryReader, you need to supply number of bytes to read from file.
                    //In this case we want to read entire file. So supplying total number of bytes.
                    data = br.ReadBytes((int)numBytes);
                }
            }

            return data;
        }

        //EHH - 12/12/09 - This will return an identifier that uses date and time only.
        public static string TimeBasedIdentifier()
        {
            return TimeBasedIdentifier("", Order.Prefix);
        }

        /* EHH - 12/12/09 - This will return an identifier that uses date and time 
         * and appends what ever the user wants as a prefix or a suffix. */
        public static string TimeBasedIdentifier(string appendThis, Order placement)
        {
            string identifier = DateTime.Now.ToString("yyyyMMddHHmmss"); //Example: 20090124191545

            if (placement == Order.Prefix)
                identifier = appendThis + identifier;
            else //Otherwise it is suffix
                identifier += appendThis;

            return identifier;
        }

        //EHH - 10/07/10 - Added
        /// <summary>
        /// This function is for getting a date based identifier
        /// </summary>
        /// <param name="appendThis">Append a string to the identifier</param>
        /// <param name="placement">Append the string to the prefix or suffix of the identifer</param>
        /// <returns>Returns the Date based identifier Ex: [prefix][Identifier][suffix]</returns>
        public static string DateBasedIdentifier(string appendThis, Order placement)
        {
            string identifier = DateTime.Today.ToString("yyyyMMdd"); //Example: 20101007

            if (placement == Order.Prefix)
                identifier = appendThis + identifier;
            else //Otherwise it is suffix
                identifier += appendThis;

            return identifier;
        }

        public enum Order
        {
            Prefix = 1,
            Suffix = 2
        };

        /* EHH - 02/24/10 - This function is dedicated to downloading the contents (usually HTML)
         * of a URL and returning it in a string. */
        public static string Download_URL_Contents(string url)
        {
            string page = string.Empty;

            WebRequest request = null;

            try
            {
                request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            page = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw; //Until I come up with a good way to track exceptions, this should be okay.
            }

            return page;
        }

        public static bool FileToString(string path, out string fileContents)
        {
            bool fileFound = false;

            fileContents = "File not found or does not exist...";

            try
            {
                fileFound = File.Exists(path);

                if (fileFound)
                    fileContents = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                throw;
            }

            return fileFound;
        }

        //EHH - 09/09/09 - Takes a string and underlines its length with "=" signs
        public static string UnderlineText(string title)
        {
            int len = 0;
            StringBuilder sb = new StringBuilder();

            sb.Append(title);
            sb.Append(CRLF);

            while (len < title.Length)
            {
                sb.Append("=");
                len++;
            }

            sb.Append(CRLF);

            return sb.ToString();
        }

        /* EHH - 09/09/09 - Takes a data table and prints it out, each column is fixed 
         * width and left justified. The columns are delimited by tabs (\t).
         * dt = The target data table to print
         * colNames = The columns in the target data table that should be printed
         * headerRow = The header row to print before the data of the data table */
        public static string PrintDataTable(DataTable dt, string[] colNames, string[] headerRow)
        {
            StringBuilder sb = new StringBuilder();
            ArrayList arrL = null;
            int[] maxColValLen = null;
            int i = 0;

            if (colNames == null)
            {
                colNames = GetColumnNamesFromDataTable(dt);

                headerRow = new string[colNames.Length];

                colNames.CopyTo(headerRow, 0);
            }

            maxColValLen = new int[colNames.Length];

            //For each column, get the max column value length
            for (i = 0; i < colNames.Length; i++) //O(c)
            {
                arrL = new ArrayList(); //Initialize an array list

                /* NOTE: There is probably a better way to get the column maximum length 
                 * of each column using dt.select or dt.compute. I tried each and all I got
                 * were run time errors. */
                //Iterate through each Data Row
                foreach (DataRow dr in dt.Rows) //O(r)
                    arrL.Add(dr[colNames[i]].ToString().Length); //Add each length to the array list

                if (headerRow != null)
                    arrL.Add(headerRow[i].Length);

                arrL.Sort(); //Sort the array ascending (ex: 0, 1, 2  ... x_n)
                maxColValLen[i] = (int)arrL[arrL.Count - 1]; //Get the last value, which should be the largest
            }

            //Print Header Content
            if (headerRow != null)
            {
                //Look through each column
                for (i = 0; i < headerRow.Length; i++) //O(c)
                    WriteCell(ref sb, headerRow[i], maxColValLen[i]);

                sb.Append(CRLF); //Terminate the line
            }

            //Print Table Content
            foreach (DataRow dr in dt.Rows) //For ever row
            {
                //Look through each column
                for (i = 0; i < colNames.Length; i++) //O(c)
                    WriteCell(ref sb, dr[colNames[i]].ToString(), maxColValLen[i]);

                sb.Append(CRLF); //Terminate the line
            }

            return sb.ToString(); //Total cost O(c*r) + O(c) + O(c) = O(c)*[O(r) + 2]
        }

        //EHH - 09/09/09 - Overloaded Function for when a header row is not needed.
        public static string PrintDataTable(DataTable dt, string[] colNames)
        {
            return PrintDataTable(dt, colNames, null);
        }

        //EHH - 03/01/10 - Overloaded Function to just print the datatable with default column names
        public static string PrintDataTable(DataTable dt)
        {
            return PrintDataTable(dt, null, null);
        }

        public static string[] GetColumnNamesFromDataTable(DataTable dt)
        {
            string[] columnNames = null;

            try
            {
                columnNames = new string[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                    columnNames[i] = dt.Columns[i].ColumnName;
            }
            catch(Exception e)
            {
                throw;
            }

            return columnNames;
        }

        //EHH - 09/09/09 - This writes the individual cells to a provided StringBuilder.
        //It will make sure to pad content according to the Maximum Column Length.
        public static void WriteCell(ref StringBuilder sbO, string columnValue, int maxColLen)
        {
            int len = 0;

            sbO.Append(columnValue); //Put it in the string

            //If the current string's length is shorter than the maximum's
            if (columnValue.Length < maxColLen)
            {
                len = columnValue.Length; //Initialize the counter to the current value's length

                //While the current length is less than the maximum length
                while (len < maxColLen)
                {
                    sbO.Append(" "); //Pad the end of the string until it is the same length
                    len++;
                }
            }

            sbO.Append("\t"); //Always pad with a tab to seperate columns
        }

        //EHH - 03/14/10 - Check if the provided string is blank or null
        public static bool IsBlankOrNull(string inputString)
        {
            return string.IsNullOrEmpty(inputString);
        }

        /*EHH - 03/21/10 - This will simply pause any Console Application
         *that calls that, it will show a message and wait for any key to
         *be pressed before the class exits. */
        public static void ConsoleApplicationPause()
        {
            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadLine();
        }

        //EHH - 10/07/10 - Added
        /// <summary>
        /// This method is for printing out a time span in the most appropriate non-zero whole integer format.
        /// Meaning: 
        ///     if there are whole minutes, then minutes will be printed,
        ///     if there are whole seconds, then seconds will be printed,
        ///     if there are whole milliseconds, then milliseconds will be printed,
        /// </summary>
        /// <param name="timeSpan">Time Span between two date times (start and stop)</param>
        /// <returns>String representation of time span in appropriate non-zero whole integer format</returns>
        public static string TimeSpanToString(TimeSpan timeSpan)
        {
            string strTimeSpan = string.Empty;

            try
            {
                if (timeSpan.Minutes > 0)
                    strTimeSpan = string.Format("{0}min", timeSpan.Minutes);
                else if (timeSpan.Seconds > 0)
                    strTimeSpan = string.Format("{0}sec", timeSpan.Seconds);
                else
                    strTimeSpan = string.Format("{0}ms", timeSpan.Milliseconds);
            }
            catch (Exception ex)
            {
                throw;
            }

            return strTimeSpan;
        }

        //EHH - 03/21/10 - This will get a character based on its ASCII char code
        // http://www.danshort.com/ASCIImap/
        public static string GetChar(byte charCode)
        {
            return Encoding.ASCII.GetChars(new byte[] { charCode })[0].ToString();
        }

        public static string CollectionToString(ICollection list)
        { 
            StringBuilder sb = null;
            
            try
            {
                sb = new StringBuilder();

                if (list != null && list.Count > 0)
                {
                    foreach (object o in list)
                    {
                        sb.Append(o.ToString());
                        sb.Append(" ");
                    }
                }
                else
                    sb.Append(string.Empty);
            }
            catch (Exception e)
            {
                throw;
            }

            return sb.ToString().Trim(); //Remove the trailing space 
        }

        /// <summary>
        /// This function wil take all extra white space in a string and change it to a single
        /// space.
        /// </summary>
        /// <param name="inputString">The input string to clean</param>
        /// <returns>Cleaned input string</returns>
        public static string WhiteSpaceToSingleSpace(string inputString)
        {
            string replacement = string.Empty;

            RegExManager rem = new RegExManager(RegExManager.WHITE_SPACE_TABS);
            rem.Initialize();

            inputString = inputString.Trim(); //Remove any starting or ending spaces

            //Replace all extra white space and Tabs with a single space 
            if(rem.ReplaceAllMatches(inputString, " ", out replacement))
                inputString = replacement;

            return inputString;
        }

        /// <summary>
        /// This is function calls the overload
        /// </summary>
        /// <returns>New Guid ID as string</returns>
        public static string GenerateCode()
        {
            return GenerateCode(-1);
        }

        /// <summary>
        /// This function generates a code based on a GUID, but only returns 
        /// a part of it or all of it depending on the input. If the requested
        /// code length is longer than the length of a guid then it will return
        /// the full guid.
        /// </summary>
        /// <param name="codeLength">The length of the code to return</param>
        /// <returns>New Partial/Whole Guid ID as string</returns>
        public static string GenerateCode(int codeLength)
        {
            string newID = Guid.NewGuid().ToString().Replace("-", string.Empty);

            if (codeLength > newID.Length || codeLength < 1)
                return newID;

            return newID.Substring(0, codeLength);
        }

        public static void WriteToFile(string fullFileNameAndPath, string fileContents)
        {
            WriteToFile(fullFileNameAndPath, fileContents, string.Empty, false);
        }

        public static void WriteToFile(string fullFileNameAndPath, string fileContents, bool appendToExistingFile)
        {
            WriteToFile(fullFileNameAndPath, fileContents, string.Empty, appendToExistingFile);
        }

        public static void WriteToFile(string fullFileNameAndPath, string fileContents, string header)
        {
            WriteToFile(fullFileNameAndPath, fileContents, header, false);
        }

        /// <summary>
        /// This function is for quickly writing text to a file.
        /// </summary>
        /// <param name="fullFileNameAndPath">The full file name and path. Example: C:\directory1\fileName.extension</param>
        /// <param name="fileContents">The contents that goes into the body of the file.</param>
        /// <param name="header">An optional header.</param>
        public static void WriteToFile(string fullFileNameAndPath, string fileContents, string header, bool appendToExistingFile)
        {
            bool blAppendToFile = false;

            try
            {
                if (!string.IsNullOrEmpty(fullFileNameAndPath))
                {
                    if(appendToExistingFile)
                        blAppendToFile = File.Exists(fullFileNameAndPath);

                    if (appendToExistingFile)
                        fileContents = CRLF + fileContents;

                    using (StreamWriter sw = new StreamWriter(fullFileNameAndPath, blAppendToFile))
                    {
                        if (!string.IsNullOrEmpty(header))
                            sw.WriteLine(header);

                        if (!string.IsNullOrEmpty(fileContents))
                            sw.WriteLine(fileContents);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.RecordException(ex);
                throw;
            }
        }

        public static string HtmlDecode(string s)
        {
            return new Utilities().HtmlDecode(s);
        }

        public static string HtmlEncode(string s)
        {
            return new Utilities().HtmlEncode(s);
        }
    }

    public class Utilities
    {
        public string HtmlDecode(string s)
        { 
            return HttpUtility.HtmlDecode(s); 
        }

        public string HtmlEncode(string s)
        {
            return HttpUtility.HtmlEncode(s);
        }
    }
}
