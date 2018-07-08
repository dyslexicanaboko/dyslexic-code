using System;
using System.IO;
using System.Threading;

namespace CfatLib
{
    public class RunHarness
    {
        private readonly string _testerName;

        private const int PauseBetweenAccessMilliseconds = 0;
        private const int WriteCycles = 10000;
        private readonly string _cycleFormat;

        public RunHarness(string testerName)
        {
            _testerName = testerName;

            _cycleFormat = GetWriteCycleFormatString(WriteCycles);
        }

        //Automatically determine the format string depending on the number of digits for the write cycles
        private static string GetWriteCycleFormatString(int number)
        {
            //Base string
            var s = string.Empty;

            //Get digit count
            var d = number.ToString().Length;

            //Format string
            var f = s.PadLeft(d, '0');

            return f;
        }

        private string GetLogEntry(int index)
        {
            var c = index + 1;

            var l = $"{c.ToString(_cycleFormat)} {_testerName} {DateTime.Now:HH:mm:ss.ffff}";

            return l;
        }

        //Got the idea from:
        //https://stackoverflow.com/questions/876473/is-there-a-way-to-check-if-a-file-is-in-use/876513#876513
        public void AccessLoop()
        {
            for (var i = 0; i < WriteCycles; i++)
            {
                Thread.Sleep(PauseBetweenAccessMilliseconds);

                var l = GetLogEntry(i);

                var f = 0;
                var success = false;

                //Keep attempting access until the resource is accessible
                //This can be adjusted to have MAX attempts to avoid an endless loop which is actually possible
                while (!success)
                {
                    try
                    {
                        FileResourceSingleton.Instance.WriteToFile(l);

                        success = true;
                    }
                    catch (IOException)
                    {
                        f++;

                        Console.WriteLine($"Failure: {f:00} -> {l}");
                    }
                }
            }
        }
    }
}
