using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace WebApplication1
{
    public class Code
    {
        [System.Web.Services.WebMethod]
        public static string GetText()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(3000);
            }

            return "All finished!";
        }

        public void ParallelTest()
        { 
            
        }
    }    
}
