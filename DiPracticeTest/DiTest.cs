using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using DiPracticeTest.SaveData;
using DiPracticeTest.DbConfig;

namespace DiPracticeTest
{
    [TestFixture]
    public class DiTest
    {
        [Ignore("old")]
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

        [Test]
        public void Can_you_use_db_to_drive_Di()
        {
            var d = new DbAppConfig();

            var c = new Container();

            //c.Register<IFood, Pizza>();
            c.Register(typeof(IFood), d.GetDependencyType(nameof(IFood)));
            c.Register(typeof(IShape), d.GetDependencyType(nameof(IShape)));

            c.Verify();

            IFood f = c.GetInstance<IFood>();
            IShape s = c.GetInstance<IShape>();

            Assert.IsTrue(f is Pizza);
            Assert.IsTrue(s is Triangle);
        }
    }
}
