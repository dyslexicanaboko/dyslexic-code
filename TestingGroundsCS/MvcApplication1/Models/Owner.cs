using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Owner
    {
        public Owner(int id)
        {
            ID = id;
            Name = ((char)(id + 65)).ToString();
            Children = new List<Owner>();
        }

        public Owner(int id, string name)
        {
            ID = id;
            Name = name;
            Children = new List<Owner>();
        }

        public int ID { get; private set; }
        public string Name { get; private set; }

        public List<Owner> Children { get; private set; }
    }
}