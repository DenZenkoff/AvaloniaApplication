using AvaloniaApplication.Database;
using AvaloniaApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace AvaloniaApplication.Queries
{
    public class TableModeQueries : DBProvider
    {
        public List<ModeModel> GetModes()
        {
            var modes = new List<ModeModel>();
            string query = "SELECT * FROM Modes";

            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                modes.Add(new ModeModel()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    MaxBottleNumber = reader.GetInt32(2),
                    MaxUsedTips = reader.GetInt32(3),
                });
            }

            return modes;
        }

        public bool DeleteMode(int modeId)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "DELETE FROM Modes WHERE ID = @ID";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ID", modeId);
            return command.ExecuteNonQuery() == 1;
        }

        public bool UpdateMode(ModeModel mode)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "UPDATE Modes SET Name = @Name, MaxBottleNumber = @MaxBottleNumber, MaxUsedTips = @MaxUsedTips WHERE ID = @ID";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", mode.Name);
            command.Parameters.AddWithValue("@MaxBottleNumber", mode.MaxBottleNumber);
            command.Parameters.AddWithValue("@MaxUsedTips", mode.MaxUsedTips);
            command.Parameters.AddWithValue("@ID", mode.Id);
            return command.ExecuteNonQuery() == 1;
        }

        public int AddMode(ModeModel mode)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string query = "INSERT INTO Modes (Name, MaxBottleNumber, MaxUsedTips) VALUES (@Name, @MaxBottleNumber, @MaxUsedTips); SELECT last_insert_rowid();";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", mode.Name);
            command.Parameters.AddWithValue("@MaxBottleNumber", mode.MaxBottleNumber);
            command.Parameters.AddWithValue("@MaxUsedTips", mode.MaxUsedTips);
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
