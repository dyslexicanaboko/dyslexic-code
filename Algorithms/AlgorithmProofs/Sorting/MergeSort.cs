using System;

namespace AlgorithmProofs.Sorting
{
    public class MergeSort
         : SortingAlgorithmBase
    {
        public MergeSort()
            : base(nameof(MergeSortArray))
        {

        }

        public override int SortingAlgorithm(int[] array)
        {
            var arrTemp = new int[array.Length];

            var loops = MergeSortArray(array, arrTemp, 0, array.Length - 1);

            return loops;
        }

        /* https://www.youtube.com/watch?v=KF2j-9iSf4Q -- her explanation is meh, but it has the code
         * https://www.youtube.com/watch?v=es2T6KY45cA -- not good for color blind people, but better representation
         * I really don't like the idea that the sort is referred to as magic, as there is no room for magic in science.
         * What happens is this is a top down algorithm, as stupid as it sounds, all of the elements are broken down
         * into single elements (feels like a waste) and then the merge operation actually does the comparisons/sorting.
         * I don't feel like this is a practical algorithm. */
        private int MergeSortArray(int[] array, int[] temp, int leftStartIndex, int rightEndIndex)
        {
            if (leftStartIndex >= rightEndIndex) return 0;

            var loops = 0;

            //Get the half way point [S       M        E]
            var middleIndex = GetMiddleIndex(leftStartIndex, rightEndIndex);

            //Sort the left half     [(S       M)        E]
            loops += MergeSortArray(array, temp, leftStartIndex, middleIndex);

            //Sort the right half    [ S       M(        E)]
            loops += MergeSortArray(array, temp, middleIndex + 1, rightEndIndex);

            //Merge back together
            loops += MergeArrays(array, temp, leftStartIndex, rightEndIndex);

            return loops;
        }

        //Transfer the left elements into the right elements since the right is the whole array always
        private int MergeArrays(int[] leftArray, int[] rightArray, int leftStartIndex, int rightEndIndex)
        {
            var loops = 0;
            //Get the half way point [0       M        N]
            var leftEndIndex = GetMiddleIndex(leftStartIndex, rightEndIndex);

            //Left Array End Index    [LS        LE             ]
            //Right Array Start Index [            RS         RE]
            var rightStartIndex = leftEndIndex + 1;

            //Size = [LS        LE RS         RE] -> RE - LS + 1
            var size = rightEndIndex - leftStartIndex + 1;

            //Copied so that they can be manipulated independently
            var i = leftStartIndex;
            var iLeft = leftStartIndex;
            var iRight = rightStartIndex;

            //This is where the actual sorting happens
            //[0    IL    LE    IR    RE    N]
            while(iLeft <= leftEndIndex && iRight <= rightEndIndex)
            {
                var e0 = leftArray[iLeft];
                var e1 = leftArray[iRight];

                //If e0 from LA is <= to e1 from LA
                if (e0 <= e1)
                {
                    //RA gets the value of e0
                    rightArray[i] = e0;

                    iLeft++;
                }
                else
                {
                    //Otherwise RA gets the value of e1
                    rightArray[i] = e1;

                    iRight++;
                }

                i++;
            }

            //Calculating the lengths so they can be reused
            var length1 = leftEndIndex - iLeft + 1;
            var length2 = rightEndIndex - iRight + 1;

            /* Source
             * Start Index
             * Destination
             * Start Index
             * Length */
            
            //Note that these copy operations sometimes end up doing nothing if the length is zero
            //LA:IL -> RA:I, for LE - IL + 1 
            Array.Copy(leftArray, iLeft, rightArray, i, length1);
            Array.Copy(leftArray, iRight, rightArray, i, length2);
            Array.Copy(rightArray, leftStartIndex, leftArray, leftStartIndex, size);

            //Aggregating the loops performed
            loops += i + length1 + length2 + size;

            return loops;
        }

        private int GetMiddleIndex(int leftStartIndex, int rightEndIndex)
        {
            var i = (leftStartIndex + rightEndIndex) / 2;

            return i;
        }
    }
}
