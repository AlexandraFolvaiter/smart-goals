using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartGoals.Models;

namespace SmartGoals.Services;

public class GoalsService
{

    private readonly DatabaseConnection _databaseConnection;

    public GoalsService()
    {
        _databaseConnection = new DatabaseConnection();
    }

    public IEnumerable<GoalModel> GetAll()
    {
        try
        {
            var connection = _databaseConnection.GetDatabaseConnection();

            var query = "SELECT * FROM [Goals]";

            var goals = new List<GoalModel>();

            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                goals.Add(new GoalModel
                {
                    Id = (Guid)reader.GetValue(0),
                    Title = (string)reader.GetValue(1),
                    Type = (string)reader.GetValue(2),
                    Status = (string)reader.GetValue(3)
                });
            }

            return goals;
        }
        catch
        {
            return new List<GoalModel>();
        }
    }

    public GoalWithDetailsModel GetById(string id)
    {
        try
        {
            var connection = _databaseConnection.GetDatabaseConnection();

            var query = $"SELECT * FROM [Goals] WHERE Id='{id}'";

            var command = new SqlCommand(query, connection);
            var reader = command.ExecuteReader();
            reader.Read();
            var goal =  new GoalWithDetailsModel
            {
                Id = (Guid)reader.GetValue(0),
                Title = (string)reader.GetValue(1),
                Type = (string)reader.GetValue(2),
                Status = (string)reader.GetValue(3),
            };

            reader.Close();

            goal.Objectives = GetObjective(connection, goal.Id);

            return goal;
        }
        catch
        {
            return null;
        }
    }

    public void AddGoal(GoalAddModel goalModel)
    {
        var connection = _databaseConnection.GetDatabaseConnection();

        var query =
            $"INSERT INTO Goals (Id,Title, Type, Status) VALUES ('{Guid.NewGuid()}', '{goalModel.Title}', '{goalModel.Type}', '{goalModel.Status}')";

        var command = new SqlCommand(query, connection);

        command.ExecuteNonQuery();
    }

    public void UpdateGoal(string id, GoalModel goalModel)
    {
        var connection = _databaseConnection.GetDatabaseConnection();

        var query = $"UPDATE Goals SET Title='{goalModel.Title}', Type='{goalModel.Type}', Status='{goalModel.Status}' WHERE Id='{id}'";

        var command = new SqlCommand(query, connection);

        command.ExecuteNonQuery();
    }

    public void DeleteGoal(string id)
    {
        var connection = _databaseConnection.GetDatabaseConnection();

        var query = $"DELETE Goals WHERE Id='{id}'";

        var command = new SqlCommand(query, connection);

        command.ExecuteNonQuery();
    }

    private IEnumerable<ObjectiveModel> GetObjective(SqlConnection connection, Guid goalId)
    {
        var query = $"SELECT * FROM [Objectives] Where GoalId='{goalId}'";

        var objectives = new List<ObjectiveModel>();

        var command = new SqlCommand(query, connection);
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            objectives.Add(new ObjectiveModel
            {
                Id = (Guid)reader.GetValue(0),
                GoalId = (Guid)reader.GetValue(1),
                Name = (string)reader.GetValue(2),
                Description = (string)reader.GetValue(3),
                TimeEstimated = (string)reader.GetValue(4),
                IsFinished = (bool)reader.GetValue(5),
                Specific = (string)reader.GetValue(6),
                Measurable = (string)reader.GetValue(7),
                Achievable = (string)reader.GetValue(8),
                Realistic = (string)reader.GetValue(9),
                Timely = (string)reader.GetValue(10),
            });
        }

        return objectives;
    }
}