using AlgorithmProofs.Sorting;
using System;
using System.IO;
using System.Linq;

namespace AlgorithmProofs
{
    public class Sequence
    {
        private readonly string RandomSequencePath;
        private const int DefaultArraySize = 10;
        private const int SeedSize = 100;
        private readonly int[] _arr;
        private readonly int _arraySize;

        public Sequence(bool generateNewSequence = false, int arraySize = DefaultArraySize)
        {
            RandomSequencePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RandomSequence.txt");

            _arraySize = arraySize;

            _arr = generateNewSequence ? 
                GenerateRandomIntegerSequence(arraySize) : 
                GetSavedSequence();

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

        private int[] GetSavedSequence()
        {
            Console.WriteLine($"Opening: {RandomSequencePath}");

            if (!File.Exists(RandomSequencePath))
                GenerateRandomIntegerSequence(_arraySize);

            var arr = File.ReadAllLines(RandomSequencePath)
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            return arr;
        }

        private int[] GenerateRandomIntegerSequence(int count)
        {
            var r = new Random();

            var arr = new int[count];

            for (int i = 0; i < count; i++)
            {
                arr[i] = r.Next(SeedSize);
            }

            var content = string.Join(Environment.NewLine, arr);

            Console.WriteLine("Sequence generated:");
            Console.WriteLine(content);

            File.WriteAllText(RandomSequencePath, content);

            return arr;
        }
    }
}
