using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class WishlistEntity
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }

        public WishlistEntity(int userId)
        {
            if (userId <= 0)
                throw new DomainException("USER_REQUIRED", "User is required");

            UserId = userId;
        }

        internal WishlistEntity(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}