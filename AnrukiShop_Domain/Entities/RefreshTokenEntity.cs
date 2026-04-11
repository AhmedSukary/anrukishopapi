using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class RefreshTokenEntity
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }

        public RefreshTokenEntity(int userId, string token, DateTime expiresAt)
        {
            if (userId <= 0)
                throw new DomainException("USER_REQUIRED", "User is required");

            if (string.IsNullOrWhiteSpace(token))
                throw new DomainException("TOKEN_EMPTY", "Refresh token is required");

            if (expiresAt < DateTime.UtcNow)
                throw new DomainException("INVALID_DATE", "Ecpires date cannot be in the past");

            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
        }

        public void Revoke()
        {
            RevokedAt = DateTime.UtcNow;
        }

        public bool IsExpired()
        {
            return DateTime.UtcNow >= ExpiresAt;
        }

        public bool IsRevoked()
        {
            return RevokedAt.HasValue;
        }

        public bool IsActive()
        {
            return !IsRevoked() && !IsExpired();
        }

        internal RefreshTokenEntity(int id, int userId, string token, DateTime expiresAt, DateTime? revokedAt)
        {
            Id = id;
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
            RevokedAt = revokedAt;
        }
    }
}