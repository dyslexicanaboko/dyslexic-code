using System;

namespace AlgorithmProofs
{
    public static class LinqPadDumpExtension
    {
        public static void Dump(this object target)
        {
            Console.WriteLine(target);
        }
        
        public static void Dump(this int[] target)
        {
            foreach (var e in target)
            {
                Console.WriteLine(e);
            }
        }
    }
}
