using System;
using Microsoft.Data.SqlClient;

namespace SmartGoals.Services;

public class DatabaseConnection
{
    public SqlConnection GetDatabaseConnection()
    {
         try
         {
             var connectionString =
                 Environment.GetEnvironmentVariable("DatabaseConnectionString", EnvironmentVariableTarget.Process);
          
             var connection = new SqlConnection(connectionString);

             connection.Open();
             return connection;
         }
         catch
         {
             return null;
         }
    }
}