using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmProofs.Sorting
{
    public class SelectionSort
        : SortingAlgorithmBase
    {
        public SelectionSort()
            : base(nameof(SelectionSort))
        {

        }

        public override int SortingAlgorithm(int[] array)
        {
            var loops = 0;

            for (int i = 0; i < array.Length; i++)
            {
                var minIndex = i;
                var minValue = array[i];

                var j = i + 1;

                if (j == array.Length) break;

                //Find the smallest element in the array
                for (; j < array.Length; j++)
                {
                    loops++;

                    var e1 = array[j];

                    //If e0 is less than or equal to e1 then keep searching
                    if (minValue <= e1) continue;

                    //Store, but keep looking
                    minIndex = j;
                    minValue = e1;
                }

                //Otherwise swap positions
                var e0 = array[i];

                array[i] = minValue;  //Promoted
                array[minIndex] = e0; //Demoted

                loops++;
            }

            return loops;
        }
    }
}
