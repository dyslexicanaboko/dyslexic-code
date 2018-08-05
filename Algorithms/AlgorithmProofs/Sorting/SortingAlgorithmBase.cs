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
    }
}
