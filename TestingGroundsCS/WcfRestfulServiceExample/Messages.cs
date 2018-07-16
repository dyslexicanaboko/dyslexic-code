using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ServiceApp
{
    [DataContract]
    public class ThreeNumbers
    {
        [DataMember(Order = 1)]
        public int First { get; set; }

        [DataMember(Order = 2)]
        public int Second { get; set; }

        [DataMember(Order = 3)]
        public int Third { get; set; }

        public int Sum()
        {
            return First + Second + Third;
        }
    }
}