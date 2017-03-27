using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary.DataAccess
{
    public class PagingQueueDAL : DAL
    {
        public enum MessageTypes { Sent = 1, Unsent = 0, All = -1 };

        public static bool InsertMessageInQueue(int subscriberID, string message, string IP_Address)
        {
            bool success = false;
            
            List<string> lstMessage = null;

            try
            {
                lstMessage = new List<string>();
                lstMessage.Add(message);

                success = InsertMessageInQueue(subscriberID, lstMessage, IP_Address);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        public static bool InsertMessageInQueue(int subscriberID, List<string> lstMessage, string IP_Address)
        { 
            bool success = false;
            
            List<int> lstSubscriberID = null;

            try
            {
                lstSubscriberID = new List<int>();
                lstSubscriberID.Add(subscriberID);

                success = InsertMessageInQueue(lstSubscriberID, lstMessage, IP_Address);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        public static bool InsertMessageInQueue(List<int> lstSubscriberID, List<string> lstMessage, string IP_Address)
        {
            bool success = false;
            
            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.PagingQueue (SubscriberID, MessageText, SenderIP) VALUES ");

                foreach (int subscriberID in lstSubscriberID)
                {
                    foreach (string message in lstMessage)
                    {
                        sb.Append("(");
                        sb.Append(subscriberID);
                        sb.Append(", ");
                        sb.Append(Utils.db_FormatString(message));
                        sb.Append(", ");
                        sb.Append(Utils.db_FormatString(IP_Address));
                        sb.Append("),");
                    }
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append(";");

                strSQLText = sb.ToString();

                success = dbm.dbNonQuery(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        private static void ListOfMessageToSQL(int subscriberID, List<string> lstMessage, string IP_Address, StringBuilder sb)
        {
            foreach (string message in lstMessage)
            {
                sb.Append("(");
                sb.Append(subscriberID);
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(message));
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(IP_Address));
                sb.Append("),");
            }
        }

        public static DataTable GetMessages(int intMessageType)
        {
            return ExecuteStoredProcedureDataTable(string.Format("usp_GetMessages @messageType = {0};", intMessageType));
        }

        public static DataTable GetMessages(MessageTypes messageType)
        {
            return GetMessages(Convert.ToInt32(messageType));
        }

        public static bool UpdatePagingQueue(DataTable dt)
        {
            bool success = false;
            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                //Foreach row in the DataTable create an update statement
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("UPDATE dbo.PagingQueue SET ");
                    sb.Append("IsSent = ");
                    sb.Append(Convert.ToInt32(dr["IsSent"]));
                    sb.Append(", DateTimeSent = ");
                    sb.Append(Utils.db_FormatString(dr["DateTimeSent"]));
                    sb.Append(", ResponseText = ");
                    sb.Append(Utils.db_FormatString(dr["ResponseText"])); 
                    sb.Append(" WHERE PagingQueueID = ");
                    sb.Append(dr["PagingQueueID"]);
                    sb.Append("; ");
                }

                strSQLText = sb.ToString();

                //Execute all of the update statements as a batch
                success = dbm.dbNonQuery(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }
    }
}
