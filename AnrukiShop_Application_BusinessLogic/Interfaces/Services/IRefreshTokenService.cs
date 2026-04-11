using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        public RefreshTokenModel? GetByToken(string token);
        public int Create(RefreshTokenModel model);
    }
}
