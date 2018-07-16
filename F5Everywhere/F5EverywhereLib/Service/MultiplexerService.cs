using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using F5EverywhereLib.Domain;
using Microsoft.SqlServer.Management.Smo;

namespace F5EverywhereLib.Service
{
    public class MultiplexerService
    {
        public bool IsServerReachable(string serverName)
        {
            bool isSuccessful = true;

            try
            {
                new Server().PingSqlServerVersion(serverName);

                isSuccessful = true;
            }
            catch
            {
                isSuccessful = false;
            }

            return isSuccessful;
        }

        public Dictionary<DatabaseInfo, ExecutionResult> ExecuteSql(IList<DatabaseInfo> databases, string sqlText)
        {
            var dict = new Dictionary<DatabaseInfo, ExecutionResult>();
            ExecutionResult r = null;

            foreach (DatabaseInfo d in databases)
            {
                r = new ExecutionResult();

                try
                {
                    r.Results = d.ExecuteSql(sqlText);
                }
                catch (Exception ex)
                {
                    r.Fail(ex);
                }

                dict.Add(d, r);
            }

            return dict;
        }

        public Dictionary<DatabaseInfo, ExecutionResult> ParseSql(IList<DatabaseInfo> databases, string sqlText)
        {
            sqlText = sqlText.Trim();

            var sb = new StringBuilder();

            //http://www.codeproject.com/Articles/410081/Parse-Transact-SQL-to-Check-Syntax
            sb.Append("SET PARSEONLY ON; ");
            sb.Append(sqlText);

            if (!sqlText.EndsWith(";"))
                sb.Append(";");

            sb.Append(" SET PARSEONLY OFF; ");

            return ExecuteSql(databases, sb.ToString());
        }
    }
}
