using System;

namespace AlgorithmProofs
{
    public class AlgorithmStats
    {
        public string AlgorithmName { get; set; }

        public int Elements { get; set; }

        public int Passes { get; set; }

        public int TotalLoops => Elements * Passes;

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public TimeSpan Elapsed { get; set; }

        public void Start()
        {
            TimeStart = DateTime.Now;
        }

        public void Stop()
        {
            TimeEnd = DateTime.Now;

            Elapsed = TimeEnd - TimeStart;
        }

        public override string ToString()
        {
            return $"Algorithm: {AlgorithmName}\nN-Passes: {Passes}\nTotal loops: {TotalLoops}\nElapsed: {Elapsed.TotalMilliseconds}ms\n";
        }
    }
}
