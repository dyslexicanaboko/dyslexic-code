using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiPracticeTest.SaveData
{
    public class SaveToFile : ISaveData
    {
        public void Save(List<string> data)
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name);
        }
    }
}
