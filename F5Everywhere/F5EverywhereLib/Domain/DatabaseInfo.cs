using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.SqlServer.Management.Smo;

namespace F5EverywhereLib.Domain
{
    public class DatabaseInfo
    {
        private Database _database;

        public DatabaseInfo(Database database)
        {
            _database = database;
        }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public List<DataTable> ExecuteSql(string sqlText)
        {
            DataTableCollection dtc = _database.ExecuteWithResults(sqlText).Tables;

            var lst = new List<DataTable>(dtc.Count);

            foreach (DataTable dt in dtc)
                lst.Add(dt);

            return lst;
        }
    }
}
