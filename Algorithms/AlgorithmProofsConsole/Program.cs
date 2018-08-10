using AlgorithmProofs;
using AlgorithmProofs.Sorting;
using System;
using System.Collections.Generic;
using AlgorithmProofs.DataStructures;

namespace AlgorithmProofsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //This has good data in it
            //https://www.quora.com/What-is-the-difference-between-a-binary-tree-and-a-binary-search-tree
            
            //This produces a fuller tree
            var bt = new BinarySearchTree(38, 13, 51, 10, 12, 40, 84, 25, 89, 37, 66, 95);

            //This produces a straight line
            //var bt = new BinarySearchTree();

            //for (int i = 1; i <= 10; i++)
            //{
            //    bt.Add(i);
            //}

            //Console.WriteLine($"{bt.Root.Number}");
            //Console.WriteLine($"{bt.Root.Left.Number}");
            //Console.WriteLine($"{bt.Root.Right.Number}");

            Console.WriteLine(bt);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static void SortingTests()
        {
            var lst = new List<SortingAlgorithmBase>
            {
                //new BubbleSort(),
                //new SelectionSort(),
                //new MergeSort(),
                //new QuickSort(),
                //new RadixSortBad(),
                new RadixSort(),
            };

            //Run the current sequence and print all
            var s = new Sequence();

            //Generate a new sequence and print all
            //var s = new Sequence(generateNewSequence: true, arraySize: 5);

            //Generate a new large sequence and don't print anything
            //var s = new Sequence(showUnsortedArray: false, generateNewSequence: true, arraySize: 1000) { ShowSortedResult = false };

            //Run the current sequence and don't print anything
            //var s = new Sequence(showUnsortedArray: false) { ShowSortedResult = false };


            lst.ForEach(x => s.TestSortAlgorithm(x));
        }
    }
}
