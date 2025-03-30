using AvaloniaApplication.Database;
using AvaloniaApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AvaloniaApplication.Queries
{
    public class TableStepQueries : DBProvider
    {
        public List<StepModel> GetSteps()
        {
            var steps = new List<StepModel>();
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "SELECT * FROM Steps";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                steps.Add(new StepModel
                {
                    Id = reader.GetInt32(0),
                    ModeId = reader.GetInt32(1),
                    Timer = reader.GetDouble(2),
                    Destination = reader.GetString(3),
                    Speed = reader.GetDouble(4),
                    Type = reader.GetString(5),
                    Volume = reader.GetDouble(6)
                });
            }
            return steps;
        }

        public int AddStep(StepModel step)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "INSERT INTO Steps (ModeId, Timer, Destination, Speed, Type, Volume) VALUES (@ModeId, @Timer, @Destination, @Speed, @Type, @Volume); SELECT last_insert_rowid();";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ModeId", step.ModeId);
            command.Parameters.AddWithValue("@Timer", step.Timer);
            command.Parameters.AddWithValue("@Destination", step.Destination);
            command.Parameters.AddWithValue("@Speed", step.Speed);
            command.Parameters.AddWithValue("@Type", step.Type);
            command.Parameters.AddWithValue("@Volume", step.Volume);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public bool UpdateStep(StepModel step)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "UPDATE Steps SET Timer = @Timer, Destination = @Destination, Speed = @Speed, Type = @Type, Volume = @Volume WHERE ID = @ID";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Timer", step.Timer);
            command.Parameters.AddWithValue("@Destination", step.Destination);
            command.Parameters.AddWithValue("@Speed", step.Speed);
            command.Parameters.AddWithValue("@Type", step.Type);
            command.Parameters.AddWithValue("@Volume", step.Volume);
            command.Parameters.AddWithValue("@ID", step.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public bool DeleteStep(int stepId)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "DELETE FROM Steps WHERE ID = @ID";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", stepId);
            return command.ExecuteNonQuery() == 1;
        }
    }
}
