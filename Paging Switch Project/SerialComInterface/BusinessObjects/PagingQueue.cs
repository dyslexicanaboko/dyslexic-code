using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PagingSwitchLibrary.DataAccess;

namespace PagingSwitchLibrary.BusinessObjects
{
    public class PagingQueue : BaseMethods
    {
        public PagingQueue()
        { 
        
        }

        public static bool InsertMessageInQueue(int subscriberID, string message, string IP_Address)
        {
            bool success = false;

            List<string> lstMessages = null;

            try
            {
                lstMessages = SplitMessageIntoMultiple(message);

                PagingQueueDAL.InsertMessageInQueue(subscriberID, lstMessages, IP_Address);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        public static bool InsertMessageInQueue(List<int> lstSubscriberID, string message, string IP_Address)
        {
            bool success = false;

            List<string> lstMessages = null;

            try
            {
                lstMessages = SplitMessageIntoMultiple(message);

                PagingQueueDAL.InsertMessageInQueue(lstSubscriberID, lstMessages, IP_Address);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        public static List<string> SplitMessageIntoMultiple(string message)
        {
            List<string> lstMessages = null;

            int intMessageLength = 0,
                intMaxMessageLength = 0,
                start = 0;

            StringBuilder sb = null;

            try
            {
                lstMessages = new List<string>();
                sb = new StringBuilder(message);
                
                intMaxMessageLength = Config.MaxMessageLength;
                
                if (intMaxMessageLength > sb.Length)
                    intMaxMessageLength = sb.Length;

                intMessageLength = intMaxMessageLength;

                while (start < sb.Length - 1)
                {
                    lstMessages.Add(sb.ToString(start, intMessageLength));

                    start = start + intMaxMessageLength;

                    if ((start + intMessageLength) > sb.Length - 1)
                        intMessageLength = sb.Length - start;
                }
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return lstMessages;
        }

        public static DataTable GetMessages(int intMessageType)
        {
            return PagingQueueDAL.GetMessages(intMessageType);
        }
        
        public static DataTable GetUnsentMessages()
        {
            return PagingQueueDAL.GetMessages(PagingQueueDAL.MessageTypes.Unsent);
        }

        public static DataTable GetSentMessages()
        {
            return PagingQueueDAL.GetMessages(PagingQueueDAL.MessageTypes.Sent);
        }

        public static DataTable GetAllMessages()
        {
            return PagingQueueDAL.GetMessages(PagingQueueDAL.MessageTypes.All);
        }
    }
}
