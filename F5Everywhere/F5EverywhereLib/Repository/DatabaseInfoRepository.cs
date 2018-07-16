using F5EverywhereLib.Domain;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace F5EverywhereLib.Repository
{
    public class DatabaseInfoRepository : RepositoryBase
    {
        public List<DatabaseInfo> GetDatabases(ServerNode server)
        {
            return GetDatabases(server.Name);
        }

        public List<DatabaseInfo> GetDatabases(string serverName)
        {
            var obj = new Server(serverName);

            return ToList(obj, obj.Databases, LoadObject);
        }

        protected List<DatabaseInfo> ToList(Server server, DatabaseCollection collection, Func<Server, Database, DatabaseInfo> method)
        {
            var lst = new List<DatabaseInfo>(collection.Count);

            foreach (Database dr in collection)
                lst.Add(method(server, dr));

            return lst;
        }

        private DatabaseInfo LoadObject(Server server, Database database)
        {
            var obj = new DatabaseInfo(database);

            obj.Name = database.Name;
            obj.ConnectionString = "Data Source=" + server + ";Initial Catalog=" + database.Name + ";Integrated Security=True;";
            
            return obj;
        }
    }
}
