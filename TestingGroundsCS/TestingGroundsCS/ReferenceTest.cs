using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingGroundsCS
{
    public static class ReferenceTest
    {
        public static void Test()
        {
            DummyFactory();
        }

        private static void AlterOrNull(ref Dummy obj)
        {
            Console.WriteLine("AlterOrNull {0}", obj.GetHashCode());

            if (obj.Age == 1)
                obj = null;
        }

        private static void AlterOrNull(Dummy obj)
        {
            obj.Age++;
        }

        private static void DummyFactory()
        {
            List<Dummy> lst = new List<Dummy>();

            lst.Add(new Dummy(1, "A"));
            lst.Add(new Dummy(1, "B"));
            lst.Add(new Dummy(1, "C"));
            lst.Add(new Dummy(1, "D"));

            foreach (Dummy d in lst)
            {
                //dum is a reference to d
                //Dummy dum = d;
                Console.WriteLine("d : {0}", d);

                //passing the reference of dum in
                AlterOrNull(d);

                Console.WriteLine("d : {0}", d);
            }
        }

        /* When making a reference to a reference type, a change to any of the references will make a change to the rest
         * since it is all pointing to the same memory location.
         * The exception to this rule is when setting one of those references to null. When you set one of those references
         * to null, all it does is set that particular reference to null and leaves the rest of them alone.*/
        public static void MultipleReferenceTypeTest()
        {
            Dummy a = new Dummy(1, "foo");
            Dummy b = a;
            Dummy c = b;
            Dummy d = c;
            Dummy e = d;

            Console.WriteLine("Initial Set");
            Print("a", a);
            Print("b", b);
            Print("c", c);
            Print("d", d);
            Print("e", e);

            a.Age = 50;

            Console.WriteLine("\nAll References Affected");
            Print("a", a);
            Print("b", b);
            Print("c", c);
            Print("d", d);
            Print("e", e);
        }

        public static void MultipleValueTypeTest()
        {
            int a = 1;
            int b = a;
            int c = b;
            int d = c;
            int e = d;

            Console.WriteLine("Initial Set");
            Print("a", a);
            Print("b", b);
            Print("c", c);
            Print("d", d);
            Print("e", e);

            a = 10;

            Console.WriteLine("\nAll References Affected");
            Print("a", a);
            Print("b", b);
            Print("c", c);
            Print("d", d);
            Print("e", e);
        }

        private static void Print(string title, Dummy d)
        {
            Console.WriteLine("{0} : {1}", title, d);
        }

        private static void Print(string title, int d)
        {
            Console.WriteLine("{0} : {1}", title, d);
        }
    }
}
