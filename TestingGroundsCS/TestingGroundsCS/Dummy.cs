using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingGroundsCS
{
    public class Dummy
    {
        public Dummy(int age, string name)
        {
            Age = age;
            Name = name;
        }

        public string Name;
        public int Age;
        public DateTime TimeStamp = DateTime.Now;

        public override string ToString()
        {
            return string.Format("N = {0}, A = {1}, H = {2}", Name, Age, this.GetHashCode());
        }
    }
}
