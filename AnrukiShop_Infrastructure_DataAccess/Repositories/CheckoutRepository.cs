using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Exceptions;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly string _connectionString;

        public CheckoutRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Checkout(int userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_Checkout", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                var outputIdParam = new SqlParameter("@NewOrderId", SqlDbType.Int)
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
                        throw new AppException("CART_NOT_FOUND", "Cart not found");

                    case 50002:
                        throw new AppException("CART_EMPTY", "Cart is empty");

                    default:
                        throw new AppException("DATABASE_ERROR", "Database error");
                }
            }
        }
    }
}
