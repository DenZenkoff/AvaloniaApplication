using System.IO;

namespace AvaloniaApplication.Database
{
    public class DBProvider
    {
        protected const string DatabaseFile = "Database\\test.db";
        protected const string ConnectionString = "Data Source=" + DatabaseFile + ";Version=3;";
    }
}
