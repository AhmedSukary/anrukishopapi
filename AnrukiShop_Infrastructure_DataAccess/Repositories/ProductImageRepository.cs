using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {

        private readonly string _connectionString;

        public ProductImageRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public ProductImageEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductImageById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new ProductImageEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetString("Url"),
                    reader.GetBoolean("IsPrimary")
                );
            }
            return null;
        }

        public List<ProductImageEntity> GetProductImagesById(int productId)
        {
            var list = new List<ProductImageEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductImagesById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", productId);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductImageEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetString("Url"),
                    reader.GetBoolean("IsPrimary")
                ));
            }
            return list;
        }

        public int Create(ProductImageEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewProductImage", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@Url", entity.Url);
            command.Parameters.AddWithValue("@IsPrimary", entity.IsPrimary);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }
        public bool Update(ProductImageEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateProductImgae", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Url", entity.Url);
            command.Parameters.AddWithValue("@IsPrimary", entity.IsPrimary);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteProductImage", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }
    }
}
