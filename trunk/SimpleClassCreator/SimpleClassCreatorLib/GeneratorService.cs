using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleClassCreator.DTO;
using SimpleClassCreator.Code_Factory;

namespace SimpleClassCreator
{
    public class GeneratorService : IGeneratorService
    {
        public ConnectionResult TestConnectionString(string connectionString)
        {
            return SimpleClassCreator.DataAccess.DAL.TestConnectionString(connectionString);
        }

        public StringBuilder BuildClass(ClassParameters parameters)
        {
            return SimpleClassCreator.Generator.Execute(parameters);
        }

        public StringBuilder BuildGridViewColumns(ClassParameters parameters)
        {
            return SimpleClassCreator.Generator.GenerateGridViewColumns(parameters);
        }

        public AssemblyInfo GetClassProperties(string assembly, string className)
        {
            return new DtoGenerator(assembly).GetClassProperties(className);
        }
    }
}
