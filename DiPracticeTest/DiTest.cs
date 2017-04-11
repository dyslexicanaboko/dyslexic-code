using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using DiPracticeTest.SaveData;

namespace DiPracticeTest
{
    [TestFixture]
    public class DiTest
    {
        [Test]
        public void Blah()
        {
            var c = new Container();

            //c.Register<ISaveData, SaveToFile>();
            c.Register<ISaveData, SaveToDb>();
            c.Verify();

            ISaveData sd = c.GetInstance<ISaveData>();

            sd.Save(null);
        }
    }
}
