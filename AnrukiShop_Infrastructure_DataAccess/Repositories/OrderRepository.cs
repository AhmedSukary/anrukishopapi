using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Enums;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public List<OrderEntity> GetAll()
        {
            var list = new List<OrderEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllOrders", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new OrderEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    (OrderStatus)reader.GetInt32("Status"),
                    reader.GetDecimal("Total"),
                    reader.GetDateTime("CreatedAt"),
                    reader.GetBoolean("IsDeleted")
                ));
            }
            return list;
        }

        public OrderEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetOrderByID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new OrderEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    (OrderStatus)reader.GetInt32("Status"),
                    reader.GetDecimal("Total"),
                    reader.GetDateTime("CreatedAt"),
                    reader.GetBoolean("IsDeleted")
                );
            }
            return null;
        }

        public List<OrderEntity> GetByUserId(int id)
        {
            var list = new List<OrderEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetOrdersByUserID", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new OrderEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId"),
                    (OrderStatus)reader.GetInt32("Status"),
                    reader.GetDecimal("Total"),
                    reader.GetDateTime("CreatedAt"),
                    reader.GetBoolean("IsDeleted")
                ));
            }
            return list;
        }

        public List<OrderItemEntity> GetItemsByOrderId(int orderId)
        {
            var list = new List<OrderItemEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetOrderItemsByOrderId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", orderId);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new OrderItemEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("OrderId"),
                    reader.GetInt32("ProductId"),
                    reader.GetInt32("Quantity"),
                    reader.GetDecimal("UnitPrice")
                ));
            }
            return list;
        }
        public bool Update(OrderEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_UpdateOrder", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Status", (int)entity.Status);
            command.Parameters.AddWithValue("@Total", entity.Total);
            command.Parameters.AddWithValue("@IsDeleted", entity.IsDeleted);
            connection.Open();

            int rows = command.ExecuteNonQuery();
            return rows > 0;
        }
    }
}
