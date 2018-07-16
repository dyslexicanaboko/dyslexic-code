using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MicroControllerConsole
{
    public static class GeneralTests
    {
        private static void WaitForRead(int seconds = 1)
        {
            for (int i = 0; i < seconds; i++)
            {
                Debug.Print(".");

                Thread.Sleep(1000); //1000 milliseconds = 1 second
            }
        }
    }
}
