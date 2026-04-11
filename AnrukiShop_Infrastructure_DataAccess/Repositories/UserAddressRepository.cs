using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly string _connectionString;

        public UserAddressRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public UserAddressEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAddressById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new UserAddressEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetString("Country"),
                    reader.GetString("City"),
                    reader.GetString("Region"),
                    reader.GetString("AddressLine"),
                    reader.GetBoolean("IsDefault")
                );
            }
            return null;
        }

        public UserAddressEntity? GetDefaultAddressByUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetDefaultAddressByUserId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", userId);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new UserAddressEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetString("Country"),
                    reader.GetString("City"),
                    reader.GetString("Region"),
                    reader.GetString("AddressLine"),
                    reader.GetBoolean("IsDefault")
                );
            }
            return null;
        }
       
        public List<UserAddressEntity> GetByUser(int userId)
        {
            var list = new List<UserAddressEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetUserAddressByUserID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", userId);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new UserAddressEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetString("Country"),
                    reader.GetString("City"),
                    reader.GetString("Region"),
                    reader.GetString("AddressLine"),
                    reader.GetBoolean("IsDefault")
                ));
            }
            return list;
        }
       
        public int Create(UserAddressEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewUserAddress", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", entity.UserId);
            command.Parameters.AddWithValue("@Country", entity.Country);
            command.Parameters.AddWithValue("@City", entity.City);
            command.Parameters.AddWithValue("@Region", entity.Region);
            command.Parameters.AddWithValue("@AddressLine", entity.AddressLine);
            command.Parameters.AddWithValue("@IsDefault", entity.IsDefault);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public bool Update(UserAddressEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateUserAddress", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Country", entity.Country);
            command.Parameters.AddWithValue("@City", entity.City);
            command.Parameters.AddWithValue("@Region", entity.Region);
            command.Parameters.AddWithValue("@AddressLine", entity.AddressLine);
            command.Parameters.AddWithValue("@IsDefault", entity.IsDefault);
            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
        
        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteUserAddress", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }
    }
}