using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        public RefreshTokenEntity? GetByToken(string token);
        public int Create(RefreshTokenEntity entity);
        public bool Update(RefreshTokenEntity entity);
    }
}
