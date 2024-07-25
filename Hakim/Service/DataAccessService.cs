using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Hakim.Service
{
    public static class DataAccessService
    {
        public static SQLiteConnection Connection { get; private set; }

        public static void SetConnection()
        {
            try
            {
                string DatabasePath = ConfigurationService.GetParentDirectoryPath(AppDomain.CurrentDomain.BaseDirectory, 0);
                var connectionString = $"Data Source={DatabasePath}\\DATA.sqlite"; // Consider moving this to a configuration file
                Connection = new SQLiteConnection(connectionString);
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while opening the connection: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (Connection != null && Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while closing the connection: {ex.Message}");
                // Handle the exception (e.g., log it or rethrow it)
            }
        }

    }
}
