using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly string _connectionString;

        public CategoryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(CategoryEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewCategory", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@ParentCategoryId", entity.ParentCategoryId == null ? DBNull.Value : entity.ParentCategoryId);

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
            using var command = new SqlCommand("SP_DeleteCategory", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }

        public List<CategoryEntity> GetAll()
        {
            var list = new List<CategoryEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllCategories", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new CategoryEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader["ParentCategoryId"] == DBNull.Value ? null : reader.GetInt32("ParentCategoryId"),
                    reader.GetBoolean("IsActive"),
                    reader.GetBoolean("IsDeleted")
                ));
            }
            return list;
        }

        public CategoryEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetCategoryByID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new CategoryEntity(
                    reader.GetInt32("Id"),
                    reader.GetString("Name"),
                    reader["ParentCategoryId"] == DBNull.Value ? null : reader.GetInt32("ParentCategoryId"),
                    reader.GetBoolean("IsActive"),
                    reader.GetBoolean("IsDeleted")
                );
            }
            return null;
        }

        public bool Update(CategoryEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateCategory", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.AddWithValue("@ParentCategoryId", entity.ParentCategoryId == null ? DBNull.Value : entity.ParentCategoryId);
            command.Parameters.AddWithValue("@IsActive", entity.IsActive);
            command.Parameters.AddWithValue("@IsDeleted", entity.IsDeleted);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
