using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace WebApplication1
{
    public partial class jQueryTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public List<KeyValue> GetBuildings()
        {
            return DummyData.GetBuildings();
        }
    }

    public class DummyData
    { 
        public static List<KeyValue> GetBuildings()
        {
            List<KeyValue> lst = new List<KeyValue>();

            lst.Add(new KeyValue("B000"));
            lst.Add(new KeyValue("B001"));
            lst.Add(new KeyValue("B002"));

            return lst;
        }

        public static List<KeyValue> GetUnits()
        {
            List<KeyValue> lst = new List<KeyValue>();

            lst.Add(new KeyValue("U000"));
            lst.Add(new KeyValue("U001"));
            lst.Add(new KeyValue("U002"));

            return lst;
        }

        public static List<KeyValue> GetResidents()
        {
            List<KeyValue> lst = new List<KeyValue>();

            lst.Add(new KeyValue("R00"));
            lst.Add(new KeyValue("R01"));
            lst.Add(new KeyValue("R02"));

            return lst;
        }
    }

    public class KeyValue
    {
        public KeyValue(string keyAndValue)
        {
            Key = keyAndValue;
            Value = keyAndValue;
        }

        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}