using System;
using System.Text;
using System.IO.Ports;

namespace MicroControllerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Communication.SendCommands();

                Console.WriteLine("Press any key to continue...");

                run = (Console.ReadLine().ToLower() == "r");
            }
        }
    }
}
