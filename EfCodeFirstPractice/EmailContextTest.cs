using EfCodeFirstPractice.Context;
using EfCodeFirstPractice.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCodeFirstPractice
{
    [TestFixture]
    public class EmailContextTest
    {
        [Test]
        public void InitializeEmailContextDatabase()
        {
            //Arrange
            using (var db = new EmailContext())
            {
                //Act
                List<Email> lst = db.Email.ToList();

                //Assert
                Assert.IsTrue(lst.Count == 0);
            }
        }
    }
}
