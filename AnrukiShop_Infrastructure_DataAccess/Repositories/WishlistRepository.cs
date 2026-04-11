using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Infrastructure.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly string _connectionString;

        public WishlistRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public int Create(WishlistEntity entity)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_AddNewWishlist", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserId", entity.UserId);

            var outputIdParam = new SqlParameter("@NewId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputIdParam);

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputIdParam.Value;
        }

        public List<WishlistEntity> GetAll()
        {
            var list = new List<WishlistEntity>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetAllWishlists", connection);

            command.CommandType = CommandType.StoredProcedure;

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new WishlistEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId")
                ));
            }
            return list;
        }

        public WishlistEntity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetWishlistById", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new WishlistEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId")
                );
            }
            return null;
        }

        public WishlistEntity? GetByUserId(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SP_GetWishlistByUserId", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new WishlistEntity(
                    reader.GetInt32("Id"),
                    reader.GetInt32("UserId")
                );
            }
            return null;
        }
    }
}
