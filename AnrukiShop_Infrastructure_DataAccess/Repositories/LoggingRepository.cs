using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly string _connectionString;

        public LoggingRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public void Log(string logLine)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewLog", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@LogLine", logLine);
  
            connection.Open();

            command.ExecuteNonQuery();
        }

        public List<string> GetLogs()
        {
            var list = new List<string>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetLogs", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();

            using var reader = command.ExecuteReader();

            while (reader.Read())            
                list.Add(reader.GetString("LogLine"));
            
            return list;
        }
    }
}
