using F5EverywhereLib.Domain;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;

namespace F5EverywhereLib.Repository
{
    public class ServerNodeRepository : RepositoryBase
    {
        public List<ServerNode> GetServers()
        {
            return ToList(SmoApplication.EnumAvailableSqlServers(false), LoadObject);
        }

        public ServerNode LoadObject(DataRow dr)
        {
            var obj = new ServerNode();

            obj.Instance = Convert.ToString(dr["Instance"]);
            obj.Name = Convert.ToString(dr["Name"]);
            obj.Server = Convert.ToString(dr["Server"]);
            obj.Version = Convert.ToString(dr["Version"]);

            return obj;
        }
    }
}
