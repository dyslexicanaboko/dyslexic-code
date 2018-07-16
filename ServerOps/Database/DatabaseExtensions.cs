using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ServerOps.Database
{
    public static class DatabaseExtensions
    {
        public static void KillConnection(this IDbConnection dbConnection)
        {
            if (dbConnection != null && dbConnection.State != ConnectionState.Closed)
            {
                dbConnection.Close();
                dbConnection.Dispose();
                dbConnection = null;
            }
        }

        public static void OpenConnection(this IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public static IDbTransaction StartTransaction(this IDbConnection connection)
        {
            OpenConnection(connection);

            return connection.BeginTransaction();
        }
    }
}
