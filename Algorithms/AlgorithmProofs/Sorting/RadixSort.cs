using System;

namespace AlgorithmProofs.Sorting
{
    public class RadixSort
        : SortingAlgorithmBase
    {
        public RadixSort()
            : base(nameof(RadixSort))
        {

        }

        //C# implementation I found
        //https://www.w3resource.com/csharp-exercises/searching-and-sorting-algorithm/searching-and-sorting-algorithm-exercise-10.php
        public override int SortingAlgorithm(int[] array)
        {
            var loops = 0;

            var tmp = new int[array.Length];

            for (var shift = 31; shift > -1; --shift)
            {
                var j = 0;

                loops++;

                for (var i = 0; i < array.Length; ++i)
                {
                    var move = (array[i] << shift) >= 0;

                    if (shift == 0 ? !move : move)
                        array[i - j] = array[i];
                    else
                        tmp[j++] = array[i];

                    loops++;
                }

                Array.Copy(tmp, 0, array, array.Length - j, j);
            }

            return loops;
        }
    }
}
