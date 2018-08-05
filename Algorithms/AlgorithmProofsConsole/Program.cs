using AlgorithmProofs;
using System;

namespace AlgorithmProofsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new BubbleSort();

            var s = new Sequence();

            s.TestSortAlgorithm(b);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
