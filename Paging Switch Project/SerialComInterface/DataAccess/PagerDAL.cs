using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary.DataAccess
{
    public class PagerDAL : DAL
    {
        public static DataTable GetPagers()
        {
            return ExecuteStoredProcedureDataTable("dbo.usp_GetPagers");
        }

        public static DataTable GetUnassignedPagers()
        {
            return ExecuteStoredProcedureDataTable("dbo.usp_GetUnassignedPagers");
        }

        public static int InsertPager(int subscriberID, int individualID, int groupID, int maicdropID, int bagID, string Notes)
        {
            int intPagerID = -1;

            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.Pagers (SubscriberID, IndividualID, GroupID, MaicdropID, BagID, AdditionalNotes) VALUES (");
                sb.Append(subscriberID);
                sb.Append(", ");
                sb.Append(individualID);
                sb.Append(", ");
                sb.Append(groupID);
                sb.Append(", ");
                sb.Append(maicdropID);
                sb.Append(", ");
                sb.Append(bagID);
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(Notes));
                sb.Append("); ");
                sb.Append(" SELECT SCOPE_IDENTITY(); ");

                strSQLText = sb.ToString();

                intPagerID = ExecuteInsert(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return intPagerID;
        }

        public static bool UpdatePager(int pagerID, int subscriberID, int bagID, string notes)
        {
            bool success = false;
            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                sb.Append("usp_UpdatePager ");
                sb.Append("@pagerID = "); sb.Append(pagerID);
                sb.Append(", @subscriberID = "); sb.Append(subscriberID);
                sb.Append(", @bagID = "); sb.Append(bagID);
                sb.Append(", @notes = "); sb.Append(Utils.db_FormatString(notes));
                //sb.Append(",@PhoneNumber = "); sb.Append(Utils.db_FormatString(PhoneNumber));
                //sb.Append(",@AdditionalInfo = "); sb.Append(Utils.db_FormatString(Notes));

                strSQLText = sb.ToString();

                success = ExecuteStoredProcedureNonQuery(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        public static bool DeletePager(int pagerID)
        {
            return ExecuteStoredProcedureNonQuery("dbo.usp_DeletePager @pagerID = " + pagerID);
        }
    }
}
