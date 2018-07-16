using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingGroundsCS
{
    public static class ActionTest
    {
        /// <summary>
        /// Call this method to run all of the provided tests.
        /// </summary>
        public static void RunAll()
        {
            ActionNoParameters();
            ActionOfT();
            FunctionOfT(); Console.WriteLine();
            UsingDelegate();
        }
        
        /// <summary>
        /// A test demonstrating the use of the Action delegate that wraps a
        /// method with no parameters or return value.
        /// </summary>
        public static void ActionNoParameters(bool async = false)
        {
            List<KeyValuePair<string, Action>> lst = new List<KeyValuePair<string, Action>>();

            lst.Add(new KeyValuePair<string, Action>("A1", MethodA));
            lst.Add(new KeyValuePair<string, Action>("A2", MethodB));
            lst.Add(new KeyValuePair<string, Action>("A3", MethodC));

            foreach (KeyValuePair<string, Action> kvp in lst)
            {
                Console.Write("Initiate : " + kvp.Key + " = ");

                if (async)
                    kvp.Value.BeginInvoke(null, null);
                else
                    kvp.Value.Invoke();
                
                Console.WriteLine();
            }
        }

        /// <summary>
        /// A test demonstrating the use of the Action Of T delegate that wraps a 
        /// method with parameters and still does not return a value.
        /// </summary>
        public static void ActionOfT()
        {
            List<ATObj> lst = new List<ATObj>();

            lst.Add(new ATObj("B1", MethodD, 1, 2));
            lst.Add(new ATObj("B2", MethodE, 3, 4));
            lst.Add(new ATObj("B3", MethodF, 5, 6));

            foreach (ATObj a in lst)
            {
                Console.Write("Initiate : " + a.Name + " = ");

                a.MethodName.Invoke(a.X, a.Y);

                Console.WriteLine();
            }
        }

        /// <summary>
        /// A test demonstrating the use of the Func Of T delegate that wraps a 
        /// method with 1 parameter and returns a single value.
        /// </summary>
        public static void FunctionOfT()
        {
            int total = 0;

            List<FTObj> lst = new List<FTObj>();

            lst.Add(new FTObj("C1", MethodJ, 1));
            lst.Add(new FTObj("C2", MethodK, 3));
            lst.Add(new FTObj("C3", MethodL, 5));

            foreach (FTObj a in lst)
            {
                Console.Write("Initiate : " + a.Name + " = ");

                total += a.MethodName.Invoke(a.X); //This returns int.

                Console.WriteLine();
            }

            Console.WriteLine("Totals: {0}", total);
        }

        /// <summary>
        /// A test demonstrating the use of a regular delegate that wraps a
        /// method with a pre-defined delegate to match. The delegate can
        /// return a value if it is defined as such.
        /// </summary>
        public static void UsingDelegate()
        {
            int total = 0;

            List<DObj> lst = new List<DObj>();

            lst.Add(new DObj("D1", MethodG, 1, 2));
            lst.Add(new DObj("D2", MethodH, 3, 4));
            lst.Add(new DObj("D3", MethodI, 5, 6));

            foreach (DObj a in lst)
            {
                Console.Write("Initiate : " + a.Name + " = ");

                total += a.MethodName.Invoke(a.X, a.Y); //This returns int.

                Console.WriteLine();
            }

            Console.WriteLine("Totals: {0}", total);
        }

        #region Parameterless with no return type
        private static void MethodA()
        {
            Console.WriteLine("Method A : {0}", DateTime.Now);
        }

        private static void MethodB()
        {
            Console.WriteLine("Method B : {0}", DateTime.Now);
        }

        private static void MethodC()
        {
            Console.WriteLine("Method C : {0}", DateTime.Now);
        }
        #endregion

        #region 2 Parameters with no return type
        private static void MethodD(int x, int y)
        {
            Console.WriteLine("Method D : {0} -> {1} + {2} = {3}", DateTime.Now, x, y, x + y);
        }

        private static void MethodE(int x, int y)
        {
            Console.WriteLine("Method E : {0} -> {1} - {2} = {3}", DateTime.Now, x, y, x - y);
        }

        private static void MethodF(int x, int y)
        {
            Console.WriteLine("Method F : {0} -> {1} x {2} = {3}", DateTime.Now, x, y, x * y);
        }
        #endregion

        #region These methods match the CustomDelegate signature
        private static int MethodG(int x, int y)
        {
            int result = x / y;

            Console.WriteLine("Method G : {0} -> {1} / {2} = {3}", DateTime.Now, x, y, result);

            return result;
        }

        private static int MethodH(int x, int y)
        {
            int result = Convert.ToInt32(Math.Pow(x + y, 2));

            Console.WriteLine("Method H : {0} -> ({1} + {2})^2 = {3}", DateTime.Now, x, y, result);

            return result;
        }

        private static int MethodI(int x, int y)
        {
            int result = Convert.ToInt32(Math.Pow(x, 2) * Math.Pow(y, 3));

            Console.WriteLine("Method I : {0} -> {1}^2 x {2}^3 = {3}", DateTime.Now, x, y, result);

            return result;
        }
        #endregion

        #region One input and One Output
        private static int MethodJ(int x)
        {
            int result = x + 1;

            Console.WriteLine("Method J : {0} -> {1} + 1 = {2}", DateTime.Now, x, result);

            return result;
        }

        private static int MethodK(int x)
        {
            int result = x - 1;

            Console.WriteLine("Method K : {0} -> {1} - 1 = {2}", DateTime.Now, x, result);

            return result;
        }

        private static int MethodL(int x)
        {
            int result = x * 2;

            Console.WriteLine("Method L : {0} -> {1} x 2 = {2}", DateTime.Now, x, result);

            return result;
        }
        #endregion

        private class ATObj
        {
            public ATObj(string name, Action<int, int> method, int x, int y)
            {
                Name = name;
                MethodName = method;
                X = x;
                Y = y;
            }

            public string Name;
            public Action<int, int> MethodName;
            public int X;
            public int Y;
        }

        private class FTObj
        {
            public FTObj(string name, Func<int, int> method, int x)
            {
                Name = name;
                MethodName = method;
                X = x;
            }

            public string Name;
            public Func<int, int> MethodName;
            public int X;
        }

        private delegate int CustomDelegate(int x, int y);

        private class DObj
        {
            public DObj(string name, CustomDelegate method, int x, int y)
            {
                Name = name;
                MethodName = method;
                X = x;
                Y = y;
            }

            public string Name;
            public CustomDelegate MethodName;
            public int X;
            public int Y;
        }
    }
}
