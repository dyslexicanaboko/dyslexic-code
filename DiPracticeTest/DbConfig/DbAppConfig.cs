using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPracticeTest.DbConfig
{
    public class DbAppConfig
    {
        private Dictionary<string, string> _dict = new Dictionary<string, string>()
        {
            { "IFood", "DiPracticeTest.DbConfig.Pizza, DiPracticeTest" },
            { "IShape", "DiPracticeTest.DbConfig.Triangle, DiPracticeTest" }
        };

        public Type GetDependencyType(string interfaceName)
        {
            //The string can be full formed FQDN or just a class name
            var t = Type.GetType(_dict[interfaceName]);

            return t;
        }
    }
}
