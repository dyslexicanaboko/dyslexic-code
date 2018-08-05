using AlgorithmProofs;
using AlgorithmProofs.Sorting;
using System;
using System.Collections.Generic;

namespace AlgorithmProofsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var lst = new List<SortingAlgorithmBase>
            {
                new BubbleSort(),
                new SelectionSort()
            };

            var s = new Sequence();

            lst.ForEach(x => s.TestSortAlgorithm(x));
            
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
