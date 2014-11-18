using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMergingToolLib
{
    class START
    {
        public static void Main(string[] args)
        {
            List<string> lst = new List<string>();
            lst.Add("File1.pdf");
            lst.Add("File2.pdf");

            //Utilities.Merge(lst, "MergeTest.pdf");

            Utilities.Collate("THI Side 1.pdf", "THI Side 2.pdf", "THI 2014.11.08.pdf");

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
