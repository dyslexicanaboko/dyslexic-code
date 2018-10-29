namespace AlgorithmProofs.Sorting
{
    public class BubbleSort
        : SortingAlgorithmBase
    {
        public BubbleSort()
            : base(nameof(BubbleSort))
        {

        }

        public override int SortingAlgorithm(int[] array)
        {
            var sorted = false;
            var passes = 0;

            while (!sorted)
            {
                //Set as sorted initially because maybe it is this time around
                sorted = true;

                for (int i = 0; i < array.Length; i++)
                {
                    passes++;

                    var e0 = array[i];

                    var j = i + 1;

                    if (j == array.Length) break;

                    var e1 = array[j];

                    //If e0 is less than or equal to e1 then continue
                    if (e0 <= e1) continue;

                    //Otherwise swap positions
                    array[i] = e1;
                    array[j] = e0;

                    sorted = false;
                }

                passes++;
            }

            return passes;
        }
    }
}
