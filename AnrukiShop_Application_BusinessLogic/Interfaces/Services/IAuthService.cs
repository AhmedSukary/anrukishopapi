using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IAuthService
    {
        public AuthModel Login(string email, string password, string ip);
        public AuthModel RefreshToken(string refreshToken);
        public bool Logout(string refreshToken);
    }
}