using System.Data.SQLite;
using System;
using AvaloniaApplication.Database;

namespace AvaloniaApplication.Queries
{
    public class TableUserQueries : DBProvider
    {
        public bool RegisterUser(string login, string password)
        {
            if (password.Length < 6 || !HasLetterAndDigit(password)) throw new Exception("'Пароль' должен содержать минимум 6 символов, 1 букву и 1 цифру.");

            if (LoginAlreadyExists(login)) throw new Exception("Такой 'Логин' уже существует.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            string sql = "INSERT INTO Users (Login, Password) VALUES (@login, @password);";

            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.ExecuteNonQuery();
            return true;
        }

        public bool AuthenticateUser(string login, string password)
        {
            string sql = "SELECT Password FROM Users WHERE Login = @login;";

            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@login", login);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                string storedHash = reader.GetString(0);
                return BCrypt.Net.BCrypt.Verify(password, storedHash);
            }

            return false;
        }

        private bool HasLetterAndDigit(string password)
        {
            var hasLetter = false;
            var hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasDigit = true;

                if (hasLetter && hasDigit) break;
            }

            return hasLetter && hasDigit;
        }

        private bool LoginAlreadyExists(string login)
        {
            string sql = "SELECT Password FROM Users WHERE Login = @login;";

            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@login", login);

            using var reader = command.ExecuteReader();
            return reader.HasRows;
        }
    }
}
