using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly string _connectionString;

        public RefreshTokenRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(RefreshTokenEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewRefreshToken", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.Parameters.AddWithValue("@Token", entity.Token);
            command.Parameters.AddWithValue("@ExpiresAt", entity.ExpiresAt);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public RefreshTokenEntity? GetByToken(string token)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetRefreshTokenByToken", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Token", token);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new RefreshTokenEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetString("Token"),
                    reader.GetDateTime("ExpiresAt"),
                    reader["RevokedAt"] == DBNull.Value ? null : reader.GetDateTime("RevokedAt")
                );
            }
            return null;
        }

        public bool Update(RefreshTokenEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateRefreshToken", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@RevokedAt", entity.RevokedAt);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
