using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using F5EverywhereLib.Domain;

namespace F5EverywhereUi
{
    public class SqlDatabaseResult
    {
        public SqlDatabaseResult()
        {
            EvaluatedResultsAsDataTables = new List<DataTable>();

            EvaluatedResultsAsDataViews = new List<DataView>();
        }

        public SqlDatabaseResult(KeyValuePair<DatabaseInfo, ExecutionResult> kvp)
        {
            Name = kvp.Key.Name;

            EvaluatedResultsAsDataTables = kvp.Value.GetEvaluatedResults();

            LoadDataViews();
        }

        public string Name { get; set; }

        public List<DataTable> EvaluatedResultsAsDataTables { get; set; }

        public List<DataView> EvaluatedResultsAsDataViews { get; set; }

        public void LoadDataViews()
        {
            if (EvaluatedResultsAsDataTables == null)
                return;

            EvaluatedResultsAsDataViews = EvaluatedResultsAsDataTables.Select(x => x.DefaultView).ToList();
        }

        public static List<SqlDatabaseResult> GetSqlResult(Dictionary<DatabaseInfo, ExecutionResult> dict)
        { 
            var lst = new List<SqlDatabaseResult>(dict.Count);

            foreach (var kvp in dict)
                lst.Add(new SqlDatabaseResult(kvp));

            return lst;
        }
    }
}
