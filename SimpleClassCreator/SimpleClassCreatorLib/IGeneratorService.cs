using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleClassCreator.DTO;
using SimpleClassCreator.Code_Factory;

namespace SimpleClassCreator
{
    public interface IGeneratorService
    {
        ConnectionResult TestConnectionString(string connectionString);

        StringBuilder BuildClass(ClassParameters parameters);

        AssemblyInfo GetClassProperties(string assembly, string className);
    }
}
