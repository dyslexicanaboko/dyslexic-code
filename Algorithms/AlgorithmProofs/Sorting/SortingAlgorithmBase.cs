using System;

namespace AlgorithmProofs.Sorting
{
    public abstract class SortingAlgorithmBase
    {
        private string _algorithmName;

        public SortingAlgorithmBase(string algorithmName)
        {
            _algorithmName = algorithmName;
        }

        public abstract int SortingAlgorithm(int[] array);

        public AlgorithmStats Sort(int[] array)
        {
            var s = new AlgorithmStats
            {
                AlgorithmName = _algorithmName,
                Elements = array.Length
            };

            s.Start();

            s.Passes = SortingAlgorithm(array);

            s.Stop();

            return s;
        }

        protected int GetMiddleIndex(int leftStartIndex, int rightEndIndex)
        {
            var i = (leftStartIndex + rightEndIndex) / 2;

            return i;
        }

        protected void SwapElements(int[] array, int leftIndex, int rightIndex)
        {
            var eL = array[leftIndex];

            array[leftIndex] = array[rightIndex];
            array[rightIndex] = eL;
        }

        protected int GetDigitLength(int number)
        {
            var length = Convert.ToInt32(Math.Ceiling(Math.Log10(number)));

            return length;
        }
    }
}
