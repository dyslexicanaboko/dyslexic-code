using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BingSearches
{
    public static class Utils
    {
        private static Logger _log;
        private static Logger Log
        {
            get
            {
                if (_log == null)
                    _log = LogManager.GetLogger("BingLogger");

                return _log;
            }
        }

        public static void LogMessage(string message)
        {
            Console.WriteLine(message);

            Log.Info(message);
        }

        public static void LogError(Exception ex, string message = null)
        {
            if (message == null)
            {
                Log.Error<Exception>(ex);

                message = string.Empty;
            }
            else
            {
                Log.Error<Exception>(message, ex);

                message += " :: ";
            }

            Console.WriteLine(message + ex.Message);
        }
    }
}
