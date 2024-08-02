using System;
using System.Data.SQLite;

namespace Hakim.Service
{
    public static class DataAccessService
    {
        public static SQLiteConnection GetConnection()
        {
            string DatabasePath = ConfigurationService.GetParentDirectoryPath(AppDomain.CurrentDomain.BaseDirectory, 0);
            var connectionString = $"Data Source={DatabasePath}\\{ConfigurationService.GetConnectionString("Database")}"; // Consider moving this to PatientDetailsDisplay configuration file
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
