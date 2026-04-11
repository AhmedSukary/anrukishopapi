using AnrukiShop_Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AnrukiShop_Infrastructure.Security
{
    public class JwtProvider : IJwtProvider
    {
        private readonly string _key;
        public JwtProvider(IConfiguration config)
        {
            _key = config["Jwt:Key"]!;
        }

        public string GenerateToken(int userId, string email, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: "AnrukiShopApi",
                audience: "Users",
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(30)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}