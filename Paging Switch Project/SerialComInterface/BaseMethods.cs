using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary
{
    public class BaseMethods
    {
        protected static string LogFileFullPath
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\_log\\" + Utils.DateBasedIdentifier("PagingSwitchLibrary_", Utils.Order.Prefix) + ".log"; }
        }

        protected static void LogException(Exception ex)
        {
            ExceptionHandler.RecordExceptionToFile(string.Empty, ex, LogFileFullPath); 
        }

        protected static void LogNotes(string notes)
        {
            ExceptionHandler.RecordNotes(notes, LogFileFullPath);
        }
    }
}
