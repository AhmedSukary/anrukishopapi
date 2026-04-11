using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {

        private readonly string _connectionString;

        public CartItemRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(CartItemEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewCartItem", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CartId", entity.CartId);
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
            command.Parameters.AddWithValue("@Price", entity.Price);

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
            using var command = new SqlCommand("SP_DeleteCartItem", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public CartItemEntity? GetByCartAndProduct(int cartId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetCartItemByCartIdAndProductId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CartId", cartId);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new CartItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("CartId"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetDecimal("Price")
                );
            }
            return null;
        }

        public List<CartItemEntity> GetByCartId(int id)
        {
            var list = new List<CartItemEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetCartItemsByCartId", connection);
            command.Parameters.AddWithValue("@Id", id);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new CartItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("CartId"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetDecimal("Price")
                ));
            }
            return list;
        }

        public CartItemEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetCartItemByID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new CartItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("CartId"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetDecimal("Price")
                );
            }
            return null;
        }

        public bool Update(CartItemEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateCartItem", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
