using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Enums;
using AnrukiShop_Application.Exceptions;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(int userId, int orderId, PaymentMethod method)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_AddNewPayment", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@Method", (int)method);

                var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outputIdParam);

                connection.Open();
                command.ExecuteNonQuery();

                return (int)outputIdParam.Value;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 50001:
                        throw new AppException("ORDER_NOT_FOUND", "Order not found");

                    case 50002:
                        throw new AppException("ORDER_EMPTY", "Order empty");

                    case 50003:
                        throw new AppException("ORDER_ALREADY_PAID", "Order already paid");

                    case 50004:
                        throw new AppException("PAYMENT_ALREADY_EXISTS", "Payment already exists");

                    default:
                        throw new AppException("DATABASE_ERROR", "Database error");
                }
            }

        }

        public PaymentEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetPaymentById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new PaymentEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("OrderId"),
                    reader.GetDecimal("Amount"),
                    (PaymentMethod)reader.GetInt32("Method"),
                    (PaymentStatus)reader.GetInt32("Status"),
                    reader["TransactionRef"] == DBNull.Value ? null : reader.GetString("TransactionRef"),
                    reader.GetDateTime("CreatedAt"),
                    reader["PaidAt"] == DBNull.Value ? null : reader.GetDateTime("PaidAt")
                );
            }
            return null;
        }

        public PaymentEntity? GetByOrderId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetPaymentByOrderId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new PaymentEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("OrderId"),
                    reader.GetDecimal("Amount"),
                    (PaymentMethod)reader.GetInt32("Method"),
                    (PaymentStatus)reader.GetInt32("Status"),
                    reader["TransactionRef"] == DBNull.Value ? null : reader.GetString("TransactionRef"),
                    reader.GetDateTime("CreatedAt"),
                    reader["PaidAt"] == DBNull.Value ? null : reader.GetDateTime("PaidAt")
                );
            }
            return null;
        }

        public bool Pay(int id, string transactionRef)
        {
            try
            {     
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_Pay", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@TransactionRef", transactionRef);

                var outputIdParam = new SqlParameter("@Success", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outputIdParam);

                connection.Open();
                command.ExecuteNonQuery();

                return (bool)outputIdParam.Value;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 50001:
                        throw new AppException("PAYMENT_NOT_FOUND", "Payment not found");

                    case 50002:
                        throw new AppException("PAYMENT_ALREADY_PAID", "Payment already paid");

                    case 50003:
                        throw new AppException("INVALID_TRANSACTION_REF", "INVALID_TRANSACTION_REF");

                    case 50004:
                        throw new AppException("PRODUCT_NOT_IN_INVENTORY", "Product not in inventory");

                    case 50005:
                        throw new AppException("INSUFFICIENT_STOCK", "Insufficient stock");

                    default:
                        throw new AppException("DATABASE_ERROR", "Database error");
                }
            }
        }
    }
}
