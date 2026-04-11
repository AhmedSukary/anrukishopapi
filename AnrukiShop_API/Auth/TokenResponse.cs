namespace AnrukiShop_API.Auth
{
    public class TokenResponse
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
