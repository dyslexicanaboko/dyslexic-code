using System;

namespace AlgorithmProofs
{
    public class AlgorithmStats
    {
        public string AlgorithmName { get; set; }

        public int Elements { get; set; }

        public int Passes { get; set; }

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
            return $"Algorithm:\t{AlgorithmName}\n" + 
                   $"Elements:\t{Elements}\n" +
                   $"N-Passes:\t{Passes}\n" +
                   $"Elapsed:\t{Elapsed.TotalMilliseconds}ms\n";
        }
    }
}
