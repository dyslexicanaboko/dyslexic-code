using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace F5EverywhereUi
{
    public class ServerManager
    {
        const int MAX_CONNECTIONS = 10;

        public List<Connection> Servers { get; private set; }
        
        public ServerManager()
        {
            Servers = LoadConnections();
        }

        private List<Connection> LoadConnections()
        {
            List<Connection> lst = new List<Connection>();

            string[] arr = Properties.Settings.Default.ConnectionsCsv.Split('|');

            foreach (string s in arr)
                lst.Add(new Connection() { ServerName = s, Verified = true });

            return lst;
        }

        public void UpdateServers(Connection target)
        {
            Connection inList = Servers.Find(x => x.ServerName == target.ServerName);

            if (inList == null && target.Verified)
            {
                //If the maximum amount of connections has been reached
                if (Servers.Count == MAX_CONNECTIONS)
                    Servers.RemoveAt(Servers.Count - 1); //Then remove the last item

                //Add the new connection to the top of the list
                Servers.Insert(0, target);

                SaveConnections();
            }
            else if (!target.Verified)
            {
                Servers.Remove(inList);

                SaveConnections();
            }
        }

        private void SaveConnections()
        {
            Properties.Settings.Default.ConnectionsCsv = string.Join("|", Servers);
            Properties.Settings.Default.Save();
        }

        public class Connection
        {
            public Connection()
            {
                ServerName = null;
                
                Verified = false;
            }

            public string ServerName { get; set; }

            public bool Verified { get; set; }

            public override string ToString()
            {
                return ServerName;
            }
        }
    }
}
