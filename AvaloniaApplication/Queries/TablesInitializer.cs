using AvaloniaApplication.Database;
using System.Data.SQLite;
using System.IO;

namespace AvaloniaApplication.Queries
{
    public class TablesInitializer : DBProvider
    {
        public static void Initialize()
        {
            if (!IsAlreadyExists())
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                CreateTables();
            }
        }

        private static bool IsAlreadyExists() => File.Exists(DatabaseFile);

        private static void CreateTables()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            string users = @"CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Login TEXT UNIQUE NOT NULL,
                Password TEXT NOT NULL
            );";

            string modes = @"CREATE TABLE IF NOT EXISTS Modes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                MaxBottleNumber INTEGER NOT NULL,
                MaxUsedTips INTEGER NOT NULL
            );";

            string steps = @"CREATE TABLE IF NOT EXISTS Steps (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ModeId INTEGER NOT NULL,
                Timer REAL NOT NULL,
                Destination TEXT NOT NULL,
                Speed REAL NOT NULL,
                Type TEXT NOT NULL,
                Volume REAL NOT NULL,
                FOREIGN KEY (ModeId) REFERENCES Modes(Id) ON DELETE CASCADE
            );";

            using var command = new SQLiteCommand(users + modes + steps, connection);
            command.ExecuteNonQuery();
        }
    }
}
