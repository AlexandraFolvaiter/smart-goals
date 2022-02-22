using System;
using Microsoft.Data.SqlClient;

namespace SmartGoals.Services;

public class DatabaseConnection
{
    public SqlConnection GetDatabaseConnection()
    {
         try
         {
             // var connectionString =
             //     Environment.GetEnvironmentVariable("DatabaseConnectionString", EnvironmentVariableTarget.Process);
             var connectionString =
                 "Server=tcp:smart-goals-demo.database.windows.net,1433;Initial Catalog=smart-goals-demo;Persist Security Info=False;User ID=smart-goals;Password=Parola1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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