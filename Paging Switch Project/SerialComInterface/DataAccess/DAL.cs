using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using ServerOps;

namespace PagingSwitchLibrary.DataAccess
{
    public class DAL : BaseMethods
    {
        protected static DataBaseManager dbm;
        protected static string strSQLText = string.Empty;

        public static void Initialize()
        {
            if (dbm == null)
            {
                string strConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

                dbm = new DataBaseManager(strConnString);
            }
        }

        protected static DataTable ExecuteStoredProcedureDataTable(string uspName)
        {
            DataTable dt = null;

            try
            {
                strSQLText = "EXEC " + uspName;

                dt = dbm.dbQueryDT(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return dt;
        }

        protected static bool ExecuteStoredProcedureNonQuery(string uspName)
        {
            bool success = false;

            try
            {
                strSQLText = "EXEC " + uspName;

                success = dbm.dbNonQuery(strSQLText);
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        protected static int ExecuteStoredProcedureInsert(string uspName)
        {
            return ExecuteInsert("EXEC " + uspName);
        }

        protected static int ExecuteInsert(string sqlCommand)
        {
            object obj = null;

            int id = -1;

            try
            {
                strSQLText = sqlCommand;

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
    }
}
