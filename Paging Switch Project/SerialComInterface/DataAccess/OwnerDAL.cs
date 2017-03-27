using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary.DataAccess
{
    public class OwnerDAL : DAL
    {
        public static DataTable GetPagerOwners()
        {
            return ExecuteStoredProcedureDataTable("dbo.usp_GetPagerOwners");
        }

        public static int InsertPagerOwner(string FirstName, string LastName, string EmailAddress, string PhoneNumber, string Notes, int PagerID)
        {
            int intPagerOwnerID = -1;

            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                sb.Append("INSERT INTO dbo.PagerOwners (PagerID, FirstName, LastName, EmailAddress, PhoneNumber, AdditionalInfo) VALUES (");
                sb.Append(PagerID);
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(FirstName));
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(LastName));
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(EmailAddress));
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(PhoneNumber));
                sb.Append(", ");
                sb.Append(Utils.db_FormatString(Notes));
                sb.Append("); ");
                sb.Append("SELECT SCOPE_IDENTITY();");

                strSQLText = sb.ToString();

                intPagerOwnerID = ExecuteInsert(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return intPagerOwnerID;
        }

        public static bool UpdatePagerOwner(string FirstName, string LastName, string EmailAddress, string PhoneNumber, string Notes, int PagerID, int PagerOwnerID)
        {
            bool success = false;
            StringBuilder sb = null;

            try
            {
                sb = new StringBuilder();

                sb.Append("usp_UpdatePagerOwner ");
                sb.Append("@PagerOwnerID = "); sb.Append(PagerOwnerID);
                sb.Append(",@PagerID = "); sb.Append(PagerID);
                sb.Append(",@FirstName = "); sb.Append(Utils.db_FormatString(FirstName)); 
                sb.Append(",@LastName = "); sb.Append(Utils.db_FormatString(LastName)); 
                sb.Append(",@EmailAddress = "); sb.Append(Utils.db_FormatString(EmailAddress));
                sb.Append(",@PhoneNumber = "); sb.Append(Utils.db_FormatString(PhoneNumber));
                sb.Append(",@AdditionalInfo = "); sb.Append(Utils.db_FormatString(Notes));            
                
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

        public static bool DeleteOwner(int ownerID)
        {
            return ExecuteStoredProcedureNonQuery("dbo.usp_DeleteOwner @ownerID = " + ownerID);
        }
    }
}
