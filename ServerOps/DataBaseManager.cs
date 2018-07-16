using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SMO = Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer;

namespace ServerOps
{
    public class DataBaseManager
    {
        //These are for testing only! Do not use them as the sole strings.
        const string AdventureWorks = @"Data Source=EUCLID\SQLEXPRESS;Initial Catalog=AdventureWorks;Persist Security Info=True;User ID=ftrack;Password=ftrack1280";
        const string ft = @"Data Source=EUCLID\SQLEXPRESS;Initial Catalog=ft;Persist Security Info=True;User ID=ftrack;Password=ftrack1280";
        
        public DataBaseManager()
        {
            dbmInit();
        }

        //This isn't necessary, but if there is a custom connection the 
        //user wants to use, they can set it already
        public DataBaseManager(string customConnectionString)
        {
            _connString = customConnectionString;
            dbmInit();
        }

        //Initialize things here for proper functionality
        private void dbmInit()
        {
            LoadAppConfigVars(); //Loads the default connection string
            
            //If the connection string is not initialized then do so and make it set to the default.
            if (_connString == string.Empty || _connString == null)
                UseDefaultConnString();
        }

        private string _connString = ""; //The Maleable Connection String - May be changed

        //This Connection string can be changed, but there will always be a fall back plan
        public string ConnectionString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        private string _defaultConnString = ""; //The immutable Connection String - May NOT be changed, only through the app.config
        
        //This is for the Connexion string located in the app.config, and cannot be changed during run time.
        public string DefaultConnectionString
        {
            get { return _defaultConnString; }
        }

        //By default the default connection string will always be used, but incase the user wants to revert
        //to the original, they can.
        public void UseDefaultConnString()
        {
            ConnectionString = DefaultConnectionString;
        }

        //Load settings from app.Config
        private void LoadAppConfigVars()
        {
            //_defaultConnString = ConfigurationSettings.AppSettings["SQLConnectionString"].ToString();
            _defaultConnString = ConfigurationManager.AppSettings["SQLConnectionString"];
            //Console.WriteLine("Default Connexion String is: {0}", _defaultConnString);
            //Console.WriteLine("test value: {0}", ConfigurationSettings.AppSettings["testValue"].ToString());
        }

        /* 
            There should be two basic types of SQL Commands:
         *  1. Non-Query
         *  2. Query
         */

        public SqlDataAdapter dbQueryDA( SqlCommand sqlCom, string dbConnStr )
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCom);
            SqlConnection sqlConn = new SqlConnection(dbConnStr);

            sqlCom.Connection = sqlConn;

            try
            {
                sqlConn.Open();
            }
            catch (Exception err)
            {
                ExceptionHandler.RecordException(err);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }

            return adapter;
        }

        //EHH - 10/05/09 - Adding overload for default conn string
        public DataTable dbQueryDT(string sqlCom)
        {
            return dbQueryDT(new SqlCommand(sqlCom), ConnectionString);
        }

        public DataTable dbQueryDT(SqlCommand sqlCom, string dbConnStr )
        {
            DataTable tempDT = new DataTable();
            SqlDataAdapter adapter = dbQueryDA(sqlCom, dbConnStr);
            adapter.Fill(tempDT);
            return tempDT;
        }

        public DataSet dbQueryDS(SqlCommand sqlCom, string dbConnStr)
        {
            DataSet tempDS = new DataSet();
            SqlDataAdapter adapter = dbQueryDA(sqlCom, dbConnStr);
            adapter.Fill(tempDS);
            return tempDS;
        }

        //EHH - 12/06/09 - I added the simplistic version of this function. 
        //It will use the default connection string.
        public bool dbNonQuery(string sqlCom)
        {
            return dbNonQuery(new SqlCommand(sqlCom), ConnectionString);
        }

        //EHH - 10/09/10 - I updated this to use transactions
        public bool dbNonQuery(SqlCommand sqlCom, string dbConnStr)
        {
            bool success = false;

            SqlConnection sqlConn = null;
            SqlTransaction trans = null;

            try
            {
                sqlConn = new SqlConnection(dbConnStr);
           
                sqlCom.Connection = sqlConn;

                sqlConn.Open();

                trans = sqlConn.BeginTransaction();
                
                sqlCom.Transaction = trans;
                sqlCom.ExecuteNonQuery();

                trans.Commit();
    
                success = true;
            }
            catch (Exception err)
            {
                trans.Rollback();
                ExceptionHandler.RecordException(err);
                throw;
            }
            finally
            {
                sqlConn.Close();
            }

            return success;
        }

        //EHH - 07/01/10 - This function is specifically for executing 
        //queries that are going to return a single scalar value
        public bool dbExecuteScalar(string sqlCom, out object scalarValue)
        { 
            DataTable dt = null;
            
            scalarValue = null;

            dt = dbQueryDT(sqlCom);

            //If there are no rows, then return false
            if (dt.Rows.Count <= 0)
                return false;

            //There should only be one row and one column!
            scalarValue = dt.Rows[0][0];

            return true;
        }

        public bool dbUpdate( string targetTable, Dictionary<string,string> cvPairs, string whereClause, string connStr )
        {
            StringBuilder sqlStrBuilder = new StringBuilder("UPDATE " + targetTable + " SET ");

            foreach (KeyValuePair<string, string> cvp in cvPairs)
                sqlStrBuilder.Append(cvp.Key + " = '" + cvp.Value + "', ");

            sqlStrBuilder.Remove(sqlStrBuilder.Length - 2, 1); //Remove the last comma.    
            sqlStrBuilder.Append("WHERE " + whereClause + "; ");

            Console.WriteLine("{0}", sqlStrBuilder.ToString());
            
            //return dbNonQuery( new SqlCommand(sqlStrBuilder.ToString(), connStr);
            return true;
        }

        public bool dbInsert( string targetTable, Dictionary<string, string> cvPairs, string connStr )
        {
            StringBuilder sqlStrBuilder = new StringBuilder("INSERT INTO " + targetTable + " ( ");

            List<string> columns = new List<string>(cvPairs.Keys);
            List<string> values = new List<string>(cvPairs.Values); 
            
            foreach (string c in columns)
                sqlStrBuilder.Append( c + ", " );

            sqlStrBuilder.Remove(sqlStrBuilder.Length - 2, 1); //Remove the last comma. 
            sqlStrBuilder.Append(") VALUES ( ");

            foreach (string v in values)
                sqlStrBuilder.Append("'" + v + "', ");

            sqlStrBuilder.Remove(sqlStrBuilder.Length - 2, 1); //Remove the last comma. 
            sqlStrBuilder.Append("); ");

            Console.WriteLine("{0}", sqlStrBuilder.ToString());

            //return dbNonQuery( new SqlCommand(sqlStrBuilder.ToString(), connStr);
            return true;
        }

        private void dbExceptionHandler(string from, Exception oops )
        {
            Console.WriteLine("Exception Occured in: {0} \n Exception: {1}", from, oops.Message );
            throw oops; //I don't have a good way to handle these yet
        }

        public static void PrintDT_Data(DataTable dt)
        {
            int i = 0 ;
            int tableWidth = dt.Columns.Count;

            if (dt.Rows.Count <= 0 || tableWidth <= 0)
                Console.WriteLine("Table is Empty");
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    for (i = 0; i < tableWidth; i++)
                        Console.Write(dr[i].ToString() + "\t");

                    Console.WriteLine();
                }
            }
        }

        //EHH - 03/14/10 - This is a simple check to make sure the provided connection string isn't bogus.
        public bool ValidateConnectionString(string dbConnStr)
        {
            bool isValid = true;

            //If the provided connexion string is bogus
            if (Utils.IsBlankOrNull(dbConnStr))
            {
                //Then use the default connection string located in the app.config
                UseDefaultConnString();
                isValid = false;
            }
            else //Otherwise, set the connexion string with what the user provided
                ConnectionString = dbConnStr;

            return isValid;
        }

        public bool DoesTableExist(string tableName)
        {
            return DoesTableExist(tableName, null, null);
        }

        public bool DoesTableExist(string tableName, string dataBaseName)
        {
            return DoesTableExist(tableName, dataBaseName, null);
        }

        //EHH - 03/14/10 - Check if the table exists already in the database. This is a shallow check only!
        public bool DoesTableExist(string tableName, string dataBaseName, string dbConnStr)
        {
            //TRUE -  means the table DOES exist already (or the name is invalid - shallow check)
            //FALSE - means the table DOES NOT exist already and may be used
            SMO.Server sqlDBServer = null;
            SMO.Database db = null;
            bool tableExists = true; //Table does exist already

            try
            {
                //Check if the table name is valid. If the table name is invalid, just return true (exists already).
                if (!Utils.IsBlankOrNull(tableName))
                {
                    ValidateConnectionString(dbConnStr);

                    using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                    {
                        sqlDBServer = new SMO.Server(new ServerConnection(sqlConn));

                        //If no data base name was provided, then get the one we are already connected to
                        if (Utils.IsBlankOrNull(dataBaseName))
                            dataBaseName = sqlConn.Database;

                        db = sqlDBServer.Databases[dataBaseName]; //Connect to the database

                        tableExists = db.Tables.Contains(tableName); //Find out if the tablename already exists
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.RecordException(e);
                throw;
            }

            return tableExists;
        }

        public string GenerateUnusedTableName()
        {
            return GenerateUnusedTableName(null, null, null);
        }

        public string GenerateUnusedTableName(string tableName)
        {
            return GenerateUnusedTableName(tableName, null, null);
        }

        public string GenerateUnusedTableName(string tableName, string dataBaseName)
        {
            return GenerateUnusedTableName(tableName, dataBaseName, null);
        }

        //EHH - 03/14/10 - Check if the table name exists already in the database. 
        public string GenerateUnusedTableName(string tableName, string dataBaseName, string dbConnStr)
        {
            //TRUE -  means the table DOES exist already (or the name is invalid - shallow check)
            //FALSE - means the table DOES NOT exist already and may be used

            SMO.Server sqlDBServer = null;
            SMO.Database db = null;
            string newTableName = string.Empty;
            int tableCount = 0;

            try
            {
                //Check if the table name is valid. If the table name is invalid, just return true (exists already).
                ValidateConnectionString(dbConnStr);

                using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlDBServer = new SMO.Server(new ServerConnection(sqlConn));

                    //If no data base name was provided, then get the one we are already connected to
                    if (Utils.IsBlankOrNull(dataBaseName))
                        dataBaseName = sqlConn.Database;

                    db = sqlDBServer.Databases[dataBaseName]; //Connect to the database

                    if (Utils.IsBlankOrNull(tableName))
                        tableName = "Table";

                    newTableName = tableName;

                    //If the tablename already exists
                    while (db.Tables.Contains(newTableName))
                    {
                        //Generate a new table name by concatenating the old name with a new number
                        newTableName = tableName + "_" + tableCount.ToString();
                        tableCount++; //Keep incrementing until an unused name is found
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.RecordException(e);
                throw;
            }

            return newTableName; //Return the new name
        }

        public bool AddTableSchemaToDataBase(DataTable dt)
        {
            return AddTableSchemaToDataBase(dt, null, null);
        }

        public bool AddTableSchemaToDataBase(DataTable dt, string dataBaseName)
        {
            return AddTableSchemaToDataBase(dt, dataBaseName, null);
        }

        public bool AddTableSchemaToDataBase(DataTable dt, string dataBaseName, string dbConnStr)
        {
            bool isSuccess = false;

            SMO.Server sqlDBServer = null;
            SMO.Database db = null;
            SMO.Table tbl = null;
            SMO.Column tempC = null;

            try
            {
                //If this table is not initialized, then don't insert it.
                if (dt == null)
                    return false;

                ValidateConnectionString(dbConnStr); //This will use the default if dbConnStr is invalid

                using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlDBServer = new SMO.Server(new ServerConnection(sqlConn));

                    //If no data base name was provided, then get the one we are already connected to
                    if (dataBaseName == null || dataBaseName == string.Empty)
                        dataBaseName = sqlConn.Database;

                    db = sqlDBServer.Databases[dataBaseName]; //Connect to the database

                    //WARNING: Make sure these datatables actually have names to give
                    tbl = new SMO.Table(db, dt.TableName);

                    //SMO Column object referring to destination table.
                    tempC = new SMO.Column();

                    //Add the column names and types from the datatable into the new table
                    //Using the columns name and type property
                    foreach (DataColumn dc in dt.Columns)
                    {
                        //Create columns from datatable column schema
                        tempC = new SMO.Column(tbl, dc.ColumnName);
                        tempC.DataType = GetDataType(dc.DataType.ToString());

                        tbl.Columns.Add(tempC);
                    }

                    //Create the Destination Table
                    tbl.Create();

                    //Create a primary key index
                    SMO.Index index = new SMO.Index(tbl, "ID");

                    index.IndexKeyType = SMO.IndexKeyType.DriPrimaryKey;
                    index.IndexedColumns.Add(new SMO.IndexedColumn(index, "ID"));

                    tbl.Indexes.Add(index);
                }

                isSuccess = true;
            }
            catch (Exception e)
            {
                ExceptionHandler.RecordException(e);
                throw;
            }

            return isSuccess;
        }

        public bool BulkCopyToDataBase(DataTable dt)
        {
            return BulkCopyToDataBase(dt, null, null);
        }

        public bool BulkCopyToDataBase(DataTable dt, string dataBaseName)
        {
            return BulkCopyToDataBase(dt, dataBaseName, null);
        }

        public bool BulkCopyToDataBase(DataTable dt, string dataBaseName, string dbConnStr)
        {
            bool isSuccess = false;

            try
            {
                //If this table is not initialized, then don't insert it.
                if (dt == null)
                    return false;

                if (dbConnStr != null)
                    ConnectionString = dbConnStr;

                //Open a connection with destination database;
                using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
                {
                    sqlConn.Open();

                    //Open bulkcopy connection.
                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlConn))
                    {
                        //Set destination table name
                        //to table previously created.
                        bulkcopy.DestinationTableName = dt.TableName;
                        bulkcopy.WriteToServer(dt);

                        sqlConn.Close();
                    }
                }

                isSuccess = true;
            }
            catch (Exception e)
            {
                ExceptionHandler.RecordException(e);
                throw;
            }

            return isSuccess;
        }

        public static SMO.DataType GetDataType(string dataType)
        {
            SMO.DataType DTTemp = null;

            switch (dataType)
            {
                case ("System.Decimal"):
                    DTTemp = SMO.DataType.Decimal(2, 18);
                    break;
                case ("System.String"):
                    DTTemp = SMO.DataType.VarChar(100);
                    break;
                case ("System.Int32"):
                    DTTemp = SMO.DataType.Int;
                    break;
            }

            return DTTemp;
        }

        static void Main(string[] args)
        {
            bool exitProg = false;
            string breakChar = string.Empty;

            DataBaseManager dbm = new DataBaseManager();

            DataTable dt = dbm.dbQueryDT("select * from dbo.Budget");

            DataBaseManager.PrintDT_Data(dt);

            string testSQLConn = @"Data Source=EUCLID\SQLEXPRESS;Initial Catalog=AdventureWorks;Persist Security Info=True;User ID=ftrack;Password=ftrack1280";
            string testCom = @"SELECT Top 5 * FROM HumanResources.Employee";

            dbm = new DataBaseManager(testSQLConn);

            dt = dbm.dbQueryDT(testCom);

            DataBaseManager.PrintDT_Data(dt);

            Console.WriteLine("\nPress Any Key To Exit...\n");

            while(!exitProg)
            {
                breakChar = Console.ReadKey(false).ToString();
                
                if(breakChar != null)
                    exitProg = true;
            }
            /*

            DataTable dt = dbm.dbQueryDT(sqlCom, AdventureWorks);

            Console.WriteLine("Print Contents: {0}", dt.Rows.Count);

            foreach( DataRow dr in dt.Rows )
                Console.WriteLine("{0} \t {1} \t {2} \t {3}", dr["EmployeeID"].ToString(), dr["NationalIDNumber"].ToString(), dr["Title"].ToString(), dr["Gender"].ToString());

            
           SqlCommand sqlCom = new SqlCommand(@"INSERT INTO dbo.Persons ( LastName, FirstName, Relation, Sex )
                                                VALUES( @ln, @fn, @Rel, @Sex)");

           sqlCom.Parameters.Add("@ln", SqlDbType.VarChar).Value = "test";
           sqlCom.Parameters.Add("@fn", SqlDbType.VarChar).Value = "test";
           sqlCom.Parameters.Add("@Rel", SqlDbType.VarChar).Value = "test";
           sqlCom.Parameters.Add("@Sex", SqlDbType.VarChar).Value = "test";

           Console.WriteLine("Insert Success? {0}", dbm.dbNonQuery(sqlCom, ft));
           */
            /*
            Dictionary<string, string> testData = new Dictionary<string, string>();
            testData.Add("LastName", "test");
            testData.Add("FirstName", "test");
            testData.Add("Relation", "test");
            testData.Add("Sex", "test");

            Console.WriteLine("Testing Insert:");
            dbm.dbInsert("dbo.Persons", testData, ft);

            Console.WriteLine("\nTesting Updates:");
            dbm.dbUpdate("dbo.Persons", testData, "P_ID = '10'", ft);
            */
        }
    }
}
