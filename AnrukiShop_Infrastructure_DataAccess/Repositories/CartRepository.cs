using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(CartEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewCart", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", entity.UserId);
         
            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteCart", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public bool Clear(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_ClearCart", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public List<CartEntity> GetAll()
        {
            var list = new List<CartEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllCarts", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new CartEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetDateTime("CreatedAt")
                ));
            }
            return list;
        }

        public CartEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetCartById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new CartEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetDateTime("CreatedAt")
                );
            }
            return null;
        }

        public CartEntity? GetByUserId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetCartByUserId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new CartEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    reader.GetDateTime("CreatedAt")
                );
            }
            return null;
        }
    }
}