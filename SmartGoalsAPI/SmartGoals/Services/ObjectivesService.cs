using System;
using Microsoft.Data.SqlClient;
using SmartGoals.Models;

namespace SmartGoals.Services;

public class ObjectivesService
{

    private readonly DatabaseConnection _databaseConnection;

    public ObjectivesService()
    {
        _databaseConnection = new DatabaseConnection();
    }
    public void Add(ObjectiveAddModel objective)
    {
            var connection = _databaseConnection.GetDatabaseConnection();

            var query =
                @$"INSERT INTO Objectives (Id, GoalId, Name, Description, TimeEstimated, IsFinished, Specific, Measurable, Achievable, Realistic, Timely)
                    VALUES ('{Guid.NewGuid()}', '{objective.GoalId}', '{objective.Name}', '{objective.Description.Replace("'", " ")}', '{objective.TimeEstimated}', '{objective.IsFinished}', '{objective.Specific}', '{objective.Measurable}', '{objective.Achievable}', '{objective.Realistic}', '{objective.Timely}')";

            var command = new SqlCommand(query, connection);

            command.ExecuteNonQuery();

        }

    public void Update(string id, ObjectiveModel objective)
    {
        var connection = _databaseConnection.GetDatabaseConnection();

        var query = @$"UPDATE Objectives SET 
                                    GoalId='{objective.GoalId}', 
                                    Name='{objective.Name}', 
                                    Description='{objective.Description.Replace("'", " ")}', 
                                    TimeEstimated='{objective.TimeEstimated}', 
                                    IsFinished='{objective.IsFinished}', 
                                    Specific='{objective.Specific}', 
                                    Measurable='{objective.Measurable}', 
                                    Achievable='{objective.Achievable}', 
                                    Realistic='{objective.Realistic}', 
                                    Timely='{objective.Timely}' 
                                WHERE Id='{id}'";

        var command = new SqlCommand(query, connection);

        command.ExecuteNonQuery();
    }

    public void UpdateIsFinished(string id, string isFinished)
    {
        var connection = _databaseConnection.GetDatabaseConnection();

        var query = $"UPDATE Objectives SET IsFinished='{isFinished}' WHERE Id='{id}'";

        var command = new SqlCommand(query, connection);

        command.ExecuteNonQuery();
    }

    public void Delete(string id)
    {
        var connection = _databaseConnection.GetDatabaseConnection();

        var query = $"DELETE Objectives WHERE Id='{id}'";

        var command = new SqlCommand(query, connection);

        command.ExecuteNonQuery();
    }
}