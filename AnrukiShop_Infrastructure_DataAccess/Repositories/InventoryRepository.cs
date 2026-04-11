using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly string _connectionString;

        public InventoryRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(InventoryEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewInventory", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
            command.Parameters.AddWithValue("@Location", entity.Location);
            command.Parameters.AddWithValue("@LastUpdated", entity.LastUpdated);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public List<InventoryEntity> GetAll()
        {
            var list = new List<InventoryEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllInventories", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new InventoryEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetString("Location"),
                    reader.GetDateTime("LastUpdated")
                ));
            }
            return list;
        }

        public InventoryEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetInventoryByID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new InventoryEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetString("Location"),
                    reader.GetDateTime("LastUpdated")
                );
            }
            return null;
        }

        public InventoryEntity? GetByProductId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetInventoryByProductID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new InventoryEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetString("Location"),
                    reader.GetDateTime("LastUpdated")
                );
            }
            return null;
        }

        public bool Update(InventoryEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateInventory", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
            command.Parameters.AddWithValue("@Location", entity.Location);
            command.Parameters.AddWithValue("@LastUpdated", entity.LastUpdated);

            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_DeleteInventory", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();

            return (int)command.ExecuteScalar() == 1;
        }
    }
}
