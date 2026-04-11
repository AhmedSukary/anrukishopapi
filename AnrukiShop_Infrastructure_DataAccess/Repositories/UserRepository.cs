using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public List<UserEntity> GetAllUsers()
        {
            var list = new List<UserEntity>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllUsers", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new UserEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Email"),
                    reader.GetString("PasswordHash"),
                    reader.GetString("FullName"),
                    reader.GetString("Role"),
                    reader.GetString("PhoneNumber"),
                    reader.GetString("Gender"),
                    reader.GetDateTime("DateOfBirth"),
                    reader.GetDateTime("CreatedAt"),
                    reader.GetBoolean("IsDeleted")
                ));
            }
            return list;
        }

        public UserEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetUserByID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new UserEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Email"),
                    reader.GetString("PasswordHash"),
                    reader.GetString("FullName"),
                    reader.GetString("Role"),
                    reader.GetString("PhoneNumber"),
                    reader.GetString("Gender"),
                    reader.GetDateTime("DateOfBirth"),
                    reader.GetDateTime("CreatedAt"),
                    reader.GetBoolean("IsDeleted")
                );
            }
            return null;
        }

        public UserEntity? GetByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetUserByEmail", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new UserEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Email"),
                    reader.GetString("PasswordHash"),
                    reader.GetString("FullName"),
                    reader.GetString("Role"),
                    reader.GetString("PhoneNumber"),
                    reader.GetString("Gender"),
                    reader.GetDateTime("DateOfBirth"),
                    reader.GetDateTime("CreatedAt"),
                    reader.GetBoolean("IsDeleted")
                );
            }
            return null;
        }

        public int Create(UserEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewUser", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            command.Parameters.AddWithValue("@FullName", entity.FullName);
            command.Parameters.AddWithValue("@Role", entity.Role);
            command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            command.Parameters.AddWithValue("@Gender", entity.Gender);
            command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public bool Update(UserEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateUser", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@PasswordHash", entity.PasswordHash);
            command.Parameters.AddWithValue("@FullName", entity.FullName);
            command.Parameters.AddWithValue("@Role", entity.Role);
            command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            command.Parameters.AddWithValue("@Gender", entity.Gender);
            command.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
            command.Parameters.AddWithValue("@IsDeleted", entity.IsDeleted);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteUser", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public bool GetEmailVerificationCode(string code)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT 1 FROM Verification WHERE Code = @Code", connection);

            command.Parameters.AddWithValue("@Code", code);

            connection.Open();

            var result = command.ExecuteScalar();

            return result != null;
        }

        public bool AddEmailVerificationCode(string code)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("INSERT INTO Verification (Code) VALUES (@Code)", connection);

            command.Parameters.AddWithValue("@Code", code);

            connection.Open();

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public bool DeleteEmailVerificationCode(string code)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("DELETE FROM Verification WHERE Code = @Code", connection);

            command.Parameters.AddWithValue("@Code", code);

            connection.Open();

            var result = command.ExecuteScalar();

            return result != null;
        }
    }
}