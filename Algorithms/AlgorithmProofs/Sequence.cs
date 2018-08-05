using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using AlgorithmProofs.Sorting;

namespace AlgorithmProofs
{
    public class Sequence
    {
        private readonly string RandomSequencePath;
        private const int DefaultArraySize = 10;
        private readonly int[] _arr;

        public Sequence()
        {
            RandomSequencePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RandomSequence.txt");

            _arr = GetTestSequence();

            //GenerateRandomIntegerSequence(10);

            Console.WriteLine("Unsorted================================");
            _arr.Dump();
            Console.WriteLine();
        }

        public void TestSortAlgorithm(SortingAlgorithmBase sortAlgorithm)
        {
            var s = sortAlgorithm.Sort(_arr);

            Console.WriteLine("Statistics =================================");
            s.Dump();

            Console.WriteLine("Sorted =================================");
            _arr.Dump();
            Console.WriteLine();
        }

        private int[] GetTestSequence()
        {
            Console.WriteLine($"Opening: {RandomSequencePath}");

            if (!File.Exists(RandomSequencePath))
                GenerateRandomIntegerSequence(DefaultArraySize);

            var arr = File.ReadAllLines(RandomSequencePath)
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            return arr;
        }

        // Define other methods and classes here
        private void GenerateRandomIntegerSequence(int count)
        {
            var r = new Random();

            var sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.AppendLine(r.Next(100).ToString());
            }

            var content = sb.ToString();

            Console.WriteLine("Sequence generated:");
            Console.WriteLine(content);

            File.WriteAllText(RandomSequencePath, content);
        }
    }
}
