using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly string _connectionString;

        public ProductReviewRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(ProductReviewEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewProductReview", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@UserName", entity.UserNmae);
            command.Parameters.AddWithValue("@Rating", entity.Rating);
            command.Parameters.AddWithValue("@Comment", entity.Comment);

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
            using var command = new SqlCommand("SP_DeleteProductReview", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public ProductReviewEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductReviewById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new ProductReviewEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetString("UserName"),
                    reader.GetInt32("Rating"),
                    reader.GetString("Comment"),
                    reader.GetDateTime("CreatedAt")
                );
            }
            return null;
        }

        public List<ProductReviewEntity> GetByProductId(int id)
        {
            var list = new List<ProductReviewEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllProductReviewByProductId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductReviewEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetString("UserName"),
                    reader.GetInt32("Rating"),
                    reader.GetString("Comment"),
                    reader.GetDateTime("CreatedAt")
                ));
            }
            return list;
        }

        public bool Update(ProductReviewEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateProductReview", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Rating", entity.Rating);
            command.Parameters.AddWithValue("@Comment", entity.Comment);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}