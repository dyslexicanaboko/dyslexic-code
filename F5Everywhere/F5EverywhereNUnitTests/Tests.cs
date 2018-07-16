using F5EverywhereLib.Domain;
using F5EverywhereLib.Repository;
using F5EverywhereLib.Service;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System;

namespace F5EverywhereNUnitTests
{
    [TestFixture]
    public class Tests
    {
        private const string SERVER = "camdevdb01";

        [Test, Ignore]
        public void When_querying_a_network_a_list_of_servers_is_returned_using_SmoApplication()
        {
            //Arrange
            DataTable dt = null;
            
            //Act
            dt = SmoApplication.EnumAvailableSqlServers(false);

            //Assert
            Assert.IsTrue(dt.Rows.Count > 0);

            /*
                Name
                Server
                Instance
                IsClustered
                Version
                IsLocal
            */
        }

        [Test, Ignore]
        public void When_querying_a_network_a_list_of_servers_is_returned_using_SqlDataSourceEnumerator()
        {
            //Arrange
            DataTable dt = null;
            var instance = SqlDataSourceEnumerator.Instance;
            
            //Act
            dt = instance.GetDataSources();

            //Assert
            Assert.IsTrue(dt.Rows.Count > 0);

            /*
                ServerName
                InstanceName
                IsClustered
                Version
            */
        }

        [Test, Ignore]
        public void When_querying_a_server_a_list_of_databases_is_returned()
        {
            //Arrange
            Server obj = null;

            //Act
            obj = new Server(SERVER);

            //Assert
            Assert.IsTrue(obj.Databases.Count > 0);
        }

        [Test, Ignore]
        public void When_querying_3_databases_then_3_results_will_be_returned()
        {
            //Arrange
            Server obj = null;
            string strSql = "SELECT 1";
            
            var lstDbs = new List<string>()
            {
                "camDev_Boca",
                "camDev_Broward",
                "camDev_Dade"
            };
            
            var lstResults = new List<DataTable>();

            //Act
            obj = new Server(SERVER);
            
            //This can be made to run in Parallel
            foreach(string db in lstDbs)
                lstResults.Add(obj.Databases[db].ExecuteWithResults(strSql).Tables[0]);

            //Assert
            Assert.IsTrue(lstResults.Count == 3);
        }

        [Test, Ignore]
        public void When_querying_3_databases_with_3_queries_then_3_sets_of_3_results_will_be_returned()
        {
            //Arrange
            string strSql = "SELECT 1; SELECT 2; SELECT 3;";
            
            List<DatabaseInfo> lstDbs = null;

            Dictionary<DatabaseInfo, ExecutionResult> dict = null;

            var mpx = new MultiplexerService();

            //{
            //    "camDev_Boca",
            //    "camDev_Broward",
            //    "camDev_Dade"
            //};


            //Act
            lstDbs = new DatabaseInfoRepository()
                .GetDatabases(SERVER)
                .Where(x => x.Name == "camDev_Boca" || x.Name == "camDev_Broward" || x.Name == "camDev_Dade")
                .ToList();

            dict = mpx.ExecuteSql(lstDbs, strSql);

            //Assert
            foreach(DatabaseInfo d in lstDbs)
                Assert.IsTrue(dict[d].Results.Count == 3);
        }

        [Test, Ignore]
        public void When_querying_a_databases_with_a_bad_queries_then_one_failure_results_will_be_returned()
        {
            //Arrange
            string strSql = "SELECT thisIsABadQuery;";

            List<DatabaseInfo> lstDbs = null;

            Dictionary<DatabaseInfo, ExecutionResult> dict = null;

            var mpx = new MultiplexerService();

            //Act
            lstDbs = new DatabaseInfoRepository()
                .GetDatabases(".\\SQLEXPRESS")
                .Where(x => x.Name == "F5Everywhere00" || x.Name == "F5Everywhere01")
                .ToList();

            dict = mpx.ExecuteSql(lstDbs, strSql);

            //Assert
            foreach (DatabaseInfo d in lstDbs)
                Assert.IsFalse(dict[d].IsSuccessful);
        }

        [Test]
        [TestCase(".\\SQLEXPRESS", true)]
        [TestCase("ThisServerDoesNotExist", false)]
        public void When_pinging_a_server_a_boolean_result_is_returned(string serverName, bool expectedResult)
        { 
            //Arrange
            bool isSuccessful = false;

            var m = new MultiplexerService();

            //Act
            isSuccessful = m.IsServerReachable(serverName);

            //Assert
            Assert.IsTrue(isSuccessful == expectedResult);
        }
    }
}
