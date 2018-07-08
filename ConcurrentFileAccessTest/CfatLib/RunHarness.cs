using System;
using System.Threading;

namespace CfatLib
{
    public class RunHarness
    {
        private readonly string _testerName;

        private const int PauseBetweenAccessMilliseconds = 0;
        private const int WriteCycles = 10000;

        public RunHarness(string testerName)
        {
            _testerName = testerName;
        }

        private string GetLogEntry(int cycle)
        {
            var l = $"{cycle:000} {_testerName} {DateTime.Now:HH:mm:ss.ffff}";

            return l;
        }

        public void AccessLoop()
        {
            for (var i = 0; i < WriteCycles; i++)
            {
                Thread.Sleep(PauseBetweenAccessMilliseconds);

                var l = GetLogEntry(i);

                FileResourceSingleton.Instance.WriteToFile(l);
            }
        }
    }
}
