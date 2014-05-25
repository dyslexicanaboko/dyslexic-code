using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleClassCreator;
using SimpleClassCreator.DTO;
using SimpleClassCreator.Code_Factory;

namespace SimpleClassCreatorUI
{
    internal static class Proxy
    {
        public static ConnectionResult TestConnectionString(string connectionString)
        {
            return Client().TestConnectionString(connectionString);
        }

        public static StringBuilder BuildClass(ClassParameters parameters)
        {
            return Client().BuildClass(parameters);
        }

        public static StringBuilder BuildGridViewColumns(ClassParameters parameters)
        {
            return Client().BuildGridViewColumns(parameters);
        }

        public static AssemblyInfo GetClassProperties(string assembly, string className)
        {
            return Client().GetClassProperties(assembly, className);
        }

        private static GeneratorService Client()
        {
            return new GeneratorService();
        }
    }
}
