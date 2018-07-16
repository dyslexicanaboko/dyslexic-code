using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proofs_UnitTests
{
    public static class START_HERE
    {
        public static void Main(string[] args)
        {
            ConfigurationTests obj = new ConfigurationTests();

            Console.WriteLine("[{0}]", obj.GetConfigValue("keyWithWhiteSpace"));
            Console.WriteLine("[{0}]", obj.GetConfigValue("keyWithPath"));
            
            string str = obj.GetConfigValue("keyThatDoesNotExist");

            Console.WriteLine("[{0}], Is Null? {1}", str, str == null);

            Console.Read();
        }
    }
}
