using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace PagingSwitchLibrary.BusinessObjects
{
    public static class Config
    {
        public static bool Debug
        {
            get 
            {
                bool blValue = false;

                if (!bool.TryParse(ConfigurationManager.AppSettings["debug"], out blValue))
                    blValue = false;

                return blValue;
            }
        }

        public static int MaxMessageLength
        {
            get
            {
                int intMaxMessageLength = 0;

                if (!int.TryParse(ConfigurationManager.AppSettings["maxMessageLength"], out intMaxMessageLength))
                    intMaxMessageLength = 500;

                return intMaxMessageLength;
            }
        }

        private static string _serialPortName;
        public static string SerialPortName
        {
            get 
            {
                if (string.IsNullOrEmpty(_serialPortName))
                    _serialPortName = ConfigurationManager.AppSettings["SerialPortName"];
                
                return _serialPortName;
            }
        }
    }
}
