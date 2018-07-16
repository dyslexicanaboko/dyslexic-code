using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace ServerOps
{
    public enum Type { EventViewer, Email, LogFile }
    
    public static class ExceptionHandler
    {
        public static void RecordNotes(string notes)
        { 
            LogException(notes, null, Type.LogFile, null);
        }

        public static void RecordNotes(string notes, string fullFilePathAndName)
        {
            LogException(notes, null, Type.LogFile, fullFilePathAndName);
        }

        public static void RecordException(Exception ex)
        {
            RecordException(null, ex);
        }

        public static void RecordException(string notes, Exception ex)
        {
            LogException(notes, ex, Type.EventViewer, null);
        }

        public static void RecordException(string notes, Exception ex, Type recordMethod)
        {
            LogException(notes, ex, recordMethod, null);
        }

        public static Exception RecordExceptionAndReturn(Exception ex)
        {
            return RecordExceptionAndReturn(null, ex);
        }

        public static Exception RecordExceptionAndReturn(string notes, Exception ex)
        {
            LogException(notes, ex, Type.EventViewer, null);

            return ex;
        }

        public static void RecordExceptionToFile(Exception ex)
        {
            LogException(null, ex, Type.LogFile, null);
        }

        public static void RecordExceptionToFile(string notes, Exception ex)
        {
            LogException(notes, ex, Type.LogFile, null);
        }

        public static void RecordExceptionToFile(string notes, Exception ex, string fullFilePathAndName)
        {
            LogException(notes, ex, Type.LogFile, fullFilePathAndName);
        }

        public static void RecordExceptionToFileAndEventLog(Exception ex)
        {
            LogException(null, ex, Type.LogFile, null);
            LogException(null, ex, Type.EventViewer, null);
        }

        public static void RecordExceptionToFileAndEventLog(string notes, Exception ex)
        {
            LogException(notes, ex, Type.LogFile, null);
            LogException(notes, ex, Type.EventViewer, null);
        }

        public static void RecordExceptionToFileAndEventLog(string notes, Exception ex, string fullFilePathAndName)
        {
            LogException(notes, ex, Type.LogFile, fullFilePathAndName);
            LogException(notes, ex, Type.EventViewer, null);
        }

        //This function handles all of the exception logging. This can only be access inside of this class.
        private static void LogException(string notes, Exception ex, Type logType, string fullFilePathAndName)
        {
            NameValueCollection nvc = null;

            try
            {
                if (string.IsNullOrEmpty(notes))
                    notes = string.Empty;

                if (ex == null)
                    ex = new Exception("Not an Exception");

                //If no log type is provided, it will default to the event viewer
                switch (logType)
                {
                    case Type.Email:
                        //Not implemented yet
                        break;

                    case Type.LogFile:
                        //If the provided path is null or empty
                        if (string.IsNullOrEmpty(fullFilePathAndName)) //Auto generate the file name and the file will be saved to the same place as the calling assembly.
                            fullFilePathAndName = Utils.DateBasedIdentifier("ErrorLog", Utils.Order.Prefix) + ".log";

                        Utils.WriteToFile(fullFilePathAndName, DateTime.Now.ToString() + "\r\n" + notes + "\r\n" + ex.ToString(), true);
                        break;

                    case Type.EventViewer:
                    default:
                        nvc = new NameValueCollection();
                        nvc.Add("Notes", notes);

                        ExceptionManager.Publish(ex, nvc);
                        break;
                }
            }
            catch 
            { 
                //fucked 
            }
        }
    }
}
