using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class WishlistItemRepository : IWishlistItemRepository
    {
        private readonly string _connectionString;

        public WishlistItemRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(WishlistItemEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewWishlistItem", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@WishlistId", entity.WishlistId);
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public bool Delete(int wishlistId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteWishlistItem", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@WishlistId", wishlistId);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public WishlistItemEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetWishlistItemById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new WishlistItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("WishlistId"),
                    reader.GetInt32("ProductId")
                );
            }
            return null;
        }

        public WishlistItemEntity? GetByWishlistAndProduct(int wishlistId, int productId)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetWishlistItemByWishlistIdAndProductId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@WishlistId", wishlistId);
            command.Parameters.AddWithValue("@ProductId", productId);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new WishlistItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("WishlistId"),
                    reader.GetInt32("ProductId")
                );
            }
            return null;
        }

        public List<WishlistItemEntity> GetByWishlistId(int id)
        {
            var list = new List<WishlistItemEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetWishlistItemsByWishlistId", connection);
            command.Parameters.AddWithValue("@Id", id);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new WishlistItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("WishlistId"),
                    reader.GetInt32("ProductId")
                ));
            }
            return list;
        }
    }
}
