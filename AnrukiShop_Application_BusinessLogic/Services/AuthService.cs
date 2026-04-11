using AnrukiShop_Application.Exceptions;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtProvider _jwt;
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        private readonly ILoggingService _loggingService;

        public AuthService
        (
            IUserRepository userRepo,
            IJwtProvider jwt,
            IRefreshTokenRepository refreshTokenRepo,
            ILoggingService loggingService
        )
        {
            _userRepo = userRepo;
            _jwt = jwt;
            _refreshTokenRepo = refreshTokenRepo;
            _loggingService = loggingService;
        }

        public AuthModel Login(string email, string password, string ip)
        {
            try
            {
                var user = _userRepo.GetByEmail(email);

                if (user == null)
                {
                    _loggingService.LogWarning(ip, "email not found", email);
                    throw new AppException("INVALID_CREDENTIALS", "Email or password incorrect");           
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    _loggingService.LogWarning(ip, "incorrect password", email);
                    throw new AppException("INVALID_CREDENTIALS", "Email or password incorrect");
                }

                var accessToken = _jwt.GenerateToken(user.Id, user.Email, user.Role);

                var refreshToken = _jwt.GenerateRefreshToken();

                var entity = new RefreshTokenEntity
                (
                    user.Id,
                    refreshToken,
                    DateTime.UtcNow.AddDays(7)
                );

                _refreshTokenRepo.Create(entity);

                _loggingService.LogInfo(ip, "logged in successfully", email);

                return new AuthModel
                {
                    UserId = user.Id,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public AuthModel RefreshToken(string refreshToken)
        {
            try
            {
                var entity = _refreshTokenRepo.GetByToken(refreshToken)
                    ?? throw new AppException("TOKEN_NOT_FOUND", "Refresh token not found");

                if (!entity.IsActive())
                    throw new AppException("TOKEN_INVALID", "Refresh token invalid");

                var user = _userRepo.GetById(entity.UserId)
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                entity.Revoke();

                _refreshTokenRepo.Update(entity);

                var newAccessToken = _jwt.GenerateToken(user.Id, user.Email, user.Role);

                var newRefreshToken = _jwt.GenerateRefreshToken();

                var newEntity = new RefreshTokenEntity
                (
                    user.Id,
                    newRefreshToken,
                    DateTime.UtcNow.AddDays(7)
                );

                _refreshTokenRepo.Create(newEntity);

                return new AuthModel
                {
                    UserId = user.Id,
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Logout(string refreshToken)
        {

            var entity = _refreshTokenRepo.GetByToken(refreshToken)
                ?? throw new AppException("TOKEN_NOT_FOUND", "Refresh token not found");

            entity.Revoke();

            _refreshTokenRepo.Update(entity);

            return true;
        }
    }
}