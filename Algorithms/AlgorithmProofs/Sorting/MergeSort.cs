using System;

namespace AlgorithmProofs.Sorting
{
    public class MergeSort
         : SortingAlgorithmBase
    {
        public MergeSort()
            : base(nameof(MergeSortM))
        {

        }

        public override int SortingAlgorithm(int[] array)
        {
            var arrTemp = new int[array.Length];

            var loops = MergeSortM(array, arrTemp, 0, array.Length - 1);

            return loops;
        }

        private int MergeSortM(int[] array, int[] temp, int leftStartIndex, int rightEndIndex)
        {
            if (leftStartIndex >= rightEndIndex) return 0;

            var loops = 0;

            //Get the half way point
            var middleIndex = GetMiddleIndex(leftStartIndex, rightEndIndex);

            loops += MergeSortM(array, temp, leftStartIndex, middleIndex);
            loops += MergeSortM(array, temp, middleIndex + 1, rightEndIndex);
            loops += MergeArrays(array, temp, leftStartIndex, rightEndIndex);

            return loops;
        }

        //Transfer the left elements into the right elements since the right is the whole array always
        private int MergeArrays(int[] leftArray, int[] rightArray, int leftStartIndex, int rightEndIndex)
        {
            var loops = 0;
            var leftEndIndex = GetMiddleIndex(leftStartIndex, rightEndIndex);
            var rightStartIndex = leftEndIndex + 1;
            var size = rightEndIndex - leftStartIndex + 1;

            var i = leftStartIndex;
            var iLeft = leftStartIndex;
            var iRight = rightStartIndex;

            while(iLeft <= leftEndIndex && iRight <= rightEndIndex)
            {
                if (leftArray[iLeft] <= leftArray[iRight])
                {
                    rightArray[i] = leftArray[iLeft];

                    iLeft++;
                }
                else
                {
                    rightArray[i] = leftArray[iRight];

                    iRight++;
                }

                i++;
            }

            //Calculating the lengths so they can be reused
            var l1 = leftEndIndex - iLeft + 1;
            var l2 = rightEndIndex - iRight + 1;

            /* Source
             * Start Index
             * Destination
             * Start Index
             * Length */
            Array.Copy(leftArray, iLeft, rightArray, i, l1);
            Array.Copy(leftArray, iRight, rightArray, i, l2);
            Array.Copy(rightArray, leftStartIndex, rightArray, leftStartIndex, size);

            //Aggregating the loops performed
            loops += i + l1 + l2 + size;

            return loops;
        }

        private int GetMiddleIndex(int leftStartIndex, int rightEndIndex)
        {
            var i = (leftStartIndex + rightEndIndex) / 2;

            return i;
        }
    }
}
