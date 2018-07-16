using System;
using System.Threading;
using Microsoft.SPOT;

namespace MFConsoleApplication1
{
    public static class OutputWindowTests
    {
        public static void OutputWindowTest()
        {
            Debug.Print(Resources.GetString(Resources.StringResources.String1));
        }

        public static void PrintRandomNumber()
        {
            int i = 1;

            Random r = new Random();
            
            while (true)
            {
                Thread.Sleep(2000);

                Debug.Print("i:[" + i + "] >> " + r.Next());

                i++;
            }
        }
    }
}
