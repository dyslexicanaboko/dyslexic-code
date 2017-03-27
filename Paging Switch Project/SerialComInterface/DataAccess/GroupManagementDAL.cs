using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary.DataAccess
{
    public class GroupManagementDAL : DAL
    {
        public static DataTable GetExistingGroups()
        {
            return ExecuteStoredProcedureDataTable("dbo.usp_GetExistingGroups");
        }

        public static DataTable GetExistingGroupAndMembers(int groupID)
        {
            return ExecuteStoredProcedureDataTable("dbo.usp_GetExistingGroupAndMembers @groupID = " + groupID);
        }
        
        public static bool UpdateGroup(int groupID, string groupDescription)
        {
            return ExecuteStoredProcedureNonQuery(string.Format("dbo.usp_UpdateGroup @groupID = {0}, @groupDescription = {1} ", groupID, Utils.db_FormatString(groupDescription)));
        }
        
        public static bool DeleteGroup(int groupID)
        {
            return ExecuteStoredProcedureNonQuery("dbo.usp_DeleteGroup @groupID = " + groupID);
        }

        public static bool DeleteGroupMembers(int groupID)
        {
            return ExecuteStoredProcedureNonQuery("dbo.usp_DeleteGroupMembers @groupID = " + groupID);
        }

        public static DataTable GetDistinctSubscriberIDs(string csvOfGroupIDs)
        {
            return ExecuteStoredProcedureDataTable(string.Format("usp_GetDistinctSubscriberIDs @lstGroupIDs = {0}", csvOfGroupIDs));
        }

        public static int InsertGroup(string groupDescription)
        {
            object obj = null;
            int id = -1;

            try
            {
                strSQLText = "EXEC dbo.usp_InsertGroup @GroupDescription = " + Utils.db_FormatString(groupDescription);

                if (dbm.dbExecuteScalar(strSQLText, out obj))
                    id = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return id;
        }

        public static bool InsertGroupMembers(int groupID, List<string> lstGroupMembers)
        {
            bool success = false;
            int i = 0;
            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.GroupMembers (GroupID, PagerOwnerID) VALUES");

                foreach (string s in lstGroupMembers)
                {
                    sb.Append("(");
                    sb.Append(groupID);
                    sb.Append(",");
                    sb.Append(s);
                    sb.Append("),");

                    i++;
                }

                if (i > 0)
                {
                    sb.Remove(sb.Length - 1, 1);

                    strSQLText = sb.ToString();

                    success = dbm.dbNonQuery(strSQLText);
                }
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
