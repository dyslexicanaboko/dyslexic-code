using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using F5EverywhereLib.Domain;

namespace F5EverywhereUi
{
    public class TargetDatabases
    {
        public TargetDatabases(string server, DatabaseInfo target)
        {
            Server = server;

            DataSource = target;

            //By default this will be true because I want the checkboxes to be checked by default
            IsSelected = true;
        }

        public bool IsSelected { get; set; }

        public string Server { get; set; }

        public string Database { get { return DataSource.Name; } }

        public DatabaseInfo DataSource { get; set; }
    }
}
