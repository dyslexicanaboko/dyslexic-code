using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AlgorithmProofs
{
    public class Sequence
    {
        private readonly string RandomSequencePath;
        private const int DefaultArraySize = 10;

        public Sequence()
        {
            RandomSequencePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RandomSequence.txt");

            //GenerateRandomIntegerSequence(10);
        }

        public void TestSortAlgorithm(ISort sortAlgorithm)
        {
            var arr = GetTestSequence();

            Console.WriteLine("Unsorted================================");
            arr.Dump();
            Console.WriteLine();

            var s = sortAlgorithm.Sort(arr);

            Console.WriteLine("Statistics =================================");
            s.Dump();

            Console.WriteLine("Sorted =================================");
            arr.Dump();
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
