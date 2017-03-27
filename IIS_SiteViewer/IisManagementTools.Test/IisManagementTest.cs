using Microsoft.Web.Administration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IisManagementTools.Test
{
    [TestFixture]
    public class IisManagementTest
    {
        [Test]
        [TestCase(8)]
        public void Querying_IIS_number_of_sites_yields_x(int numberOfSitesExpected)
        {
            using (var svc = new IisManagementService())
            {
                //Assign
                int numberOfSites = 0;
                SiteCollection col = null;

                //Act
                col = svc.GetSites();
                numberOfSites = col.Count;

                //Assert
                Assert.AreEqual(numberOfSites, numberOfSitesExpected);
            }
        }

        [Test]
        [TestCase(6)]
        public void Querying_IIS_number_of_default_documents_yields_x(int numberOfDefaultDocumentsExpected)
        {
            using (var svc = new IisManagementService())
            {
                //Assign
                int numberOfDefaultDocuments = 0;
                Site site = null;

                //Act
                site = svc.GetSites()[1];
                numberOfDefaultDocuments = svc.GetDefaultDocuments(site).Count;

                //Assert
                Assert.AreEqual(numberOfDefaultDocuments, numberOfDefaultDocumentsExpected);
            }
        }

        [Test]
        public void Querying_IIS_get_sites()
        {
            using (var svc = new IisManagementService())
            {
                //Assign
                svc.GetSitesDirectory();

                //Act

                //Assert
            }
        }

        [Test]
        public void Change_application_pool_credentials()
        {
            using (var svc = new IisManagementService())
            {
                //Assign
                string strUsername = "eli";
                string strPassword = "***REMOVED***";

                List<Creds> lstBefore = null;
                List<Creds> lstAfter = null;

                //Act
                lstBefore = svc.GetApplicationPools(ProcessModelIdentityType.SpecificUser).Select(x => new Creds(x.Name, x.ProcessModel)).ToList();

                //Assert
                svc.ChangeCredentials(strUsername, strPassword);

                lstAfter = svc.GetApplicationPools(ProcessModelIdentityType.SpecificUser).Select(x => new Creds(x.Name, x.ProcessModel)).ToList();

                for (int i = 0; i < lstBefore.Count; i++)
                {
                    Assert.AreEqual(lstBefore[i], lstAfter[i]);
                }
            }
        }

        private class Creds : IEqualityComparer<Creds>
        {
            public Creds(string pool, ApplicationPoolProcessModel processModel)
            {
                Pool = pool;
                User = processModel.UserName;
                Pass = processModel.Password;
            }

            public string Pool { get; set; }
            public string User { get; set; }
            public string Pass { get; set; }

            public bool Equals(Creds x, Creds y)
            {
                return x.Pool == y.Pool
                    && x.User == y.User
                    && x.Pass == y.Pass;
            }

            public int GetHashCode(Creds obj)
            {
                return base.GetHashCode();
            }
        }
    }
}
