using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repo;

        public RefreshTokenService(IRefreshTokenRepository repo)
        {
            _repo = repo;
        }

        public int Create(RefreshTokenModel model)
        {
            try
            {
                var entity = new RefreshTokenEntity(
                    model.UserId,
                    model.Token,
                    model.ExpiresAt
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public RefreshTokenModel? GetByToken(string token)
        {
            try
            {
                var entity = _repo.GetByToken(token)
                    ?? throw new AppException("TOKEN_NOT_FOUND", "Token not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        } 
    }
}
