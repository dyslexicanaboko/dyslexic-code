using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using ServerOps;

namespace TestingGroundsCS
{
    public class Program
    {
        #region Older Stuff
        public Program()
        { 
        
        }

        public static void FormatDate()
        {
            Console.WriteLine(DateTime.Today.ToString("yyyy-MM-dd"));
        }

        public void AnchorTest()
        {
            string input = "<a href=\"http://www.economist.com/blogs/democracyinamerica/2010/03/economist_yougov_polling\">YouGov/Polimetrix</a>",
                   output = string.Empty,
                   strRegex = "</*?([A ][A-Z0-9=\"/]*)\b[^>]*>";

            RegExManager rem = new RegExManager(RegExManager.ANCHOR);

            rem.Initialize();
            
            if (rem.ReplaceAllMatches(input, string.Empty, out output))
                Console.WriteLine(output);
            else
                Console.WriteLine("BAD!");
        }

        public static void LoopingCollectionTest()
        {
            List<string> lst = new List<string>();
            lst.Add("b01");
            lst.Add("b02");
            lst.Add("b03");

            LoopingCollection lc = new LoopingCollection(lst);

            for (int i = 0; i < 10; i++)
                Console.WriteLine(lc.Next());
        }

        public static void ParentWork(int workForInSeconds)
        {
            for (int i = 0; i < workForInSeconds; i++)
            {
                Console.WriteLine("Parent Sleeping: {0}", (i + 1));
                Thread.Sleep(1000);
            }
        }

        public static void TimmyTest()
        {
            //Thread timmy = new Thread(new ThreadStart(tt.ThreadStubMethod)); //(tt.ThreadClassWork);
            //Thread timmy = new Thread(new ParameterizedThreadStart(tt.ThreadClassWork)); //(tt.ThreadClassWork);

            //Console.WriteLine("Parent - Start\n");
            //timmy.Start();

            //while (!tt.IsTimmyFinished)
            //{
            //    //Don't do anything, wait for timmy to finish
            //}

            ParentWork(5);

            Console.WriteLine(Assembly.GetExecutingAssembly().Location + "\\");
            Console.WriteLine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            Console.WriteLine();
            Console.WriteLine("Parent - End");
            
        }
        #endregion



        static void Main(string[] args)
        {
            Console.WriteLine("Start\n");

            //ReferenceTest.Test();
            //ReferenceTest.MultipleValueTypeTest();
            BingSearches.PerformSearches(2, 40);
            //BingSearches.Blah();

            Console.WriteLine("\nEnd");

            //DO NOT REMOVE
            Utils.ConsoleApplicationPause();
        }
    }
}
