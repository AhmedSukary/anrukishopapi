namespace AnrukiShop_Application.Models
{
    public class AuthModel
    {
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}