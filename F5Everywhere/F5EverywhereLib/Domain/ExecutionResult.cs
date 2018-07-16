using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace F5EverywhereLib.Domain
{
    public class ExecutionResult
    {
        public ExecutionResult()
        {
            IsSuccessful = true;

            Results = new List<DataTable>();
        }

        public List<DataTable> Results { get; set; }

        public bool IsSuccessful { get; set; }

        public Exception SqlError { get; set; }

        public List<DataTable> EvaluatedResults { get { return GetEvaluatedResults(); } }

        public void Fail(Exception exception)
        {
            IsSuccessful = false;

            SqlError = exception;
        }

        public List<DataTable> GetEvaluatedResults()
        { 
            List<DataTable> lst = null;

            if (IsSuccessful)
            {
                if (Results.Count == 0)
                {
                    lst = new List<DataTable>() 
                    { 
                        GetScalarAsDataTable("Result", "The query you executed did not return any results, but executed successfully.")
                    };
                }
                else
                    lst = Results;
            }
            else
            {
                lst = new List<DataTable>() 
                { 
                    GetFailureAsDataTable()
                };
            }

            return lst;
        }

        private DataTable GetFailureAsDataTable()
        {
            return GetScalarAsDataTable("ErrorMessage", GetFailureMessage());
        }

        private DataTable GetScalarAsDataTable(string column, string value)
        {
            var dt = new DataTable();
            dt.Columns.Add(column, typeof(string));

            DataRow dr = dt.NewRow();

            dr[column] = value;

            dt.Rows.Add(dr);

            return dt;
        }

        public string GetFailureMessage()
        {
            if (SqlError == null)
                return string.Empty;

            var sb = new StringBuilder();

            GetErrorMessage(sb, SqlError);

            return sb.ToString();
        }

        private void GetErrorMessage(StringBuilder sb, Exception ex)
        {
            //FailedOperationException
            //ExecutionFailureException
            //SqlException

            if(!(ex is FailedOperationException) && !(ex is ExecutionFailureException))
                sb.AppendLine(ex.Message);

            if (ex.InnerException != null)
                GetErrorMessage(sb, ex.InnerException);
        }
    }
}
