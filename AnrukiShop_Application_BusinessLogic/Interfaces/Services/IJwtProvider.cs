namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IJwtProvider
    {
        public string GenerateToken(int userId, string email, string role);
        public string GenerateRefreshToken();
    }
}