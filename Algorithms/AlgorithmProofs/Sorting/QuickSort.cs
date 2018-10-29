namespace AlgorithmProofs.Sorting
{
    public class QuickSort
        : SortingAlgorithmBase
    {
        public QuickSort()
            : base(nameof(QuickSort))
        {

        }

        private int _loops = 0;

        public override int SortingAlgorithm(int[] array)
        {
            var loops = QuickSortArray(array, 0, array.Length - 1);

            loops += _loops;

            return loops;
        }

        /* The one mistake I keep seeing being made in all explanations is that the VALUE is what is random
         * and is what is the pivot. How the value is chosen is far from random. The middle index is always
         * chosen, the value itself will never be known though.
         * https://www.youtube.com/watch?v=SLauY6PpjW4 */
        private int QuickSortArray(int[] array, int leftIndex, int rightIndex)
        {
            //[0     RI LI      N]
            if (leftIndex >= rightIndex) return 0;

            var loops = 0;

            var i = Partition(array, leftIndex, rightIndex);

            var j = i - 1;

            if (leftIndex < j)
            {
                loops += QuickSortArray(array, leftIndex, j);
            }

            if (i < rightIndex)
            {
                loops += QuickSortArray(array, i, rightIndex);
            }

            loops++;

            return loops;
        }

        /* Once again, just like the Merge Sort - the actual sorting is happening in this method. 
         * The sorting is happening by going from the outside in essentially. A new pivot value
         * is chosen each time this method is accessed. */
        private int Partition(int[] array, int leftIndex, int rightIndex)
        {
            //The middle of the array is always chosen
            var middleIndex = GetMiddleIndex(leftIndex, rightIndex);

            //This element is considered random since we don't know what is located at this position
            var ePivot = array[middleIndex];

            //[0     LI RI     N]
            while (leftIndex <= rightIndex)
            {
                var eL = array[leftIndex];

                //[0     EL EP     N]
                while (eL < ePivot)
                {
                    leftIndex++;

                    eL = array[leftIndex];

                    _loops++;
                }

                var eR = array[rightIndex];

                //[0     EP ER     N]
                while (eR > ePivot)
                {
                    rightIndex--;

                    eR = array[rightIndex];

                    _loops++;
                }

                //[0     LI RI     N]
                if (leftIndex <= rightIndex)
                {
                    SwapElements(array, leftIndex, rightIndex);

                    leftIndex++;

                    rightIndex--;
                }

                _loops++;
            }

            return leftIndex;
        }
    }
}
