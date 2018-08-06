using System.Collections.Generic;
using System.Linq;

namespace AlgorithmProofs.Sorting
{
    public class RadixSortBad
        : SortingAlgorithmBase
    {
        public RadixSortBad()
            : base(nameof(RadixSortBad))
        {

        }

        //https://www.youtube.com/watch?v=XiuSW_mEn7g -- best explanation I have seen
        //This is my crappy attempt at making a Radix Sort - the theory is correct, but it is laughably inefficient
        public override int SortingAlgorithm(int[] array)
        {
            var loops = 0;

            /* 1. To start with look at every number by its last digit 
             * 2. Sort by that last number 
             * 3. Move to the left digit and repeat step 2
             *      If a number doesn't have a digit in that position it is zero. */

            //This is the k portion of O(k*n)
            var maxDigitCount = GetMaxDigitCount(array);

            for (var i = 0; i < maxDigitCount; i++)
            {
                loops++;

                var dict = new SortedDictionary<int, List<int>>();

                foreach (var v in array)
                {
                    var k = GetNthDigit(i, v);

                    IndexDigit(dict, k, v);

                    loops++;
                }

                var j = 0;

                //Recreate array in order
                foreach (var kvp in dict)
                {
                    foreach (var e in kvp.Value)
                    {
                        array[j] = e;

                        j++;
                        loops++;
                    }

                    loops++;
                }
            }

            return loops;
        }

        private void IndexDigit(SortedDictionary<int, List<int>> dictionary, int key, int number)
        {
            var d = dictionary;

            if (d.ContainsKey(key))
            {
                d[key].Add(number);
            }
            else
            {
                d.Add(key, new List<int>() { number });
            }
        }

        private int GetNthDigit(int position, int number)
        {
            var p = (position + 1) * 10;

            var l1 = GetDigitLength(p);
            var l2 = GetDigitLength(number);

            if (l1 > l2) return 0;

            var n = (number % p);

            return n;
        }

        private int GetMaxDigitCount(int[] array)
        {
            var m = array.Max();

            var r = GetDigitLength(m);

            return r;
        }
    }
}
