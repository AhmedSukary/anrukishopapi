using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class CartEntity
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public CartEntity(int userId)
        {
            if (userId <= 0)
                throw new DomainException("USER_REQUIRED", "User is required");

            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        internal CartEntity(int id, int userId, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}