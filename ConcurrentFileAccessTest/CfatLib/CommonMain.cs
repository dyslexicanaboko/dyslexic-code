using System;

namespace CfatLib
{
    public class CommonMain
    {
        public CommonMain()
        {
            Console.WriteLine("Press enter to begin");
            Console.ReadLine();

            while (true)
            {
                var s = AppDomain.CurrentDomain.FriendlyName;

                Console.WriteLine($"Running harness for: {s}");

                var r = new RunHarness(s);

                r.AccessLoop();

                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
