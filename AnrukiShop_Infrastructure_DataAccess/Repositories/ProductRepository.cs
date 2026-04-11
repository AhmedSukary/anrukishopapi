using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public ProductEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductByID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new ProductEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader.GetString("Description"),
                    reader.GetDecimal("Price"),
                    reader.GetString("SKU"),
                    reader.GetInt32("CategoryId"),
                    reader.GetBoolean("IsActive"),
                    reader.GetBoolean("IsDeleted"),
                    reader.GetDateTime("CreatedAt")
                );
            }
            return null;
        }

        public ProductSummaryModel? GetSummaryById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductSummary", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new ProductSummaryModel(
                      reader.GetInt32("Id"),
                      reader.GetString("Name"),
                      reader.GetString("Description"),
                      reader.GetDecimal("Price"),
                      reader["PrimaryImageUrl"] == DBNull.Value ? null : reader.GetString("PrimaryImageUrl"),
                      reader["AvgRating"] == DBNull.Value ? null : reader.GetInt32("AvgRating"),
                      reader["CommentsCount"] == DBNull.Value ? null : reader.GetInt32("CommentsCount")
                  );
            }
            return null;
        }

        public List<ProductEntity> GetAll()
        {
            var list = new List<ProductEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllProducts", connection);

            command.CommandType = CommandType.StoredProcedure;


            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader.GetString("Description"),
                    reader.GetDecimal("Price"),
                    reader.GetString("SKU"),
                    reader.GetInt32("CategoryId"),
                    reader.GetBoolean("IsActive"),
                    reader.GetBoolean("IsDeleted"),
                    reader.GetDateTime("CreatedAt")
                ));
            }
            return list;
        }

        public List<ProductSummaryModel> GetProductsSummary()
        {
            var list = new List<ProductSummaryModel>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductsSummary", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductSummaryModel(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader.GetString("Description"),
                    reader.GetDecimal("Price"),
                    reader["PrimaryImageUrl"] == DBNull.Value ? null : reader.GetString("PrimaryImageUrl"),
                    reader["AvgRating"] == DBNull.Value ? null : reader.GetInt32("AvgRating"),
                    reader["CommentsCount"] == DBNull.Value ? null : reader.GetInt32("CommentsCount")
                ));
            }
            return list;
        }

        public List<ProductSummaryModel> GetProductsSummaryByCategoryId(int id)
        {
            var list = new List<ProductSummaryModel>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductsSummaryByCategoryId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CategoryId", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductSummaryModel(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader.GetString("Description"),
                    reader.GetDecimal("Price"),
                    reader["PrimaryImageUrl"] == DBNull.Value ? null : reader.GetString("PrimaryImageUrl"),
                    reader["AvgRating"] == DBNull.Value ? null : reader.GetInt32("AvgRating"),
                    reader["CommentsCount"] == DBNull.Value ? null : reader.GetInt32("CommentsCount")
                ));
            }
            return list;
        }

        public List<ProductSummaryModel> SearchProducts(string query)
        {
            var list = new List<ProductSummaryModel>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_SearchProducts", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Query", query);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductSummaryModel(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader.GetString("Description"),
                    reader.GetDecimal("Price"),
                    reader["PrimaryImageUrl"] == DBNull.Value ? null : reader.GetString("PrimaryImageUrl"),
                    reader["AvgRating"] == DBNull.Value ? null : reader.GetInt32("AvgRating"),
                    reader["CommentsCount"] == DBNull.Value ? null : reader.GetInt32("CommentsCount")
                ));
            }
            return list;
        }

        public List<ProductEntity> GetByCategoryId(int id)
        {
            var list = new List<ProductEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetProductsByCategoryId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CategoryId", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new ProductEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader.GetString("Description"),
                    reader.GetDecimal("Price"),
                    reader.GetString("SKU"),
                    reader.GetInt32("CategoryId"),
                    reader.GetBoolean("IsActive"),
                    reader.GetBoolean("IsDeleted"),
                    reader.GetDateTime("CreatedAt")
                ));
            }
            return list;
        }

        public int Create(ProductEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewProduct", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Description", entity.Description);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@SKU", entity.SKU);
            command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public bool Update(ProductEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateProduct", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@Description", entity.Description);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@SKU", entity.SKU);
            command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
            command.Parameters.AddWithValue("@IsActive", entity.IsActive);
            command.Parameters.AddWithValue("@IsDeleted", entity.IsDeleted);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteProduct", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }
    }
}
