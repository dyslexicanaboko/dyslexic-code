using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;

namespace ServiceApp
{
    public class AmazingService : IAmazing
    {
        public string Ping()
        {
            return "Pong @ " + DateTime.Now.ToString();
        }

        public int AddTwoNumbers(int x, int y)
        {
            return x + y;
        }

        public int AddThreeNumbers(ThreeNumbers numbers)
        {
            return numbers.Sum();
        }

        public bool SyncOperation(string jsonString)
        {
            return true;
        }

        public string CopyCat(string input)
        {
            return "Copy Cat Says: " + input;
        }
    }
}