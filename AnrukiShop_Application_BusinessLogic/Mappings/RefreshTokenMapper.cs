using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Mappings
{
    public static class RefreshTokenMapper
    {
        public static RefreshTokenModel ToModel(this RefreshTokenEntity entity)
        {
            return new RefreshTokenModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Token = entity.Token,            
                ExpiresAt =  entity.ExpiresAt,
                RevokedAt = entity.RevokedAt
            };
        }

        public static List<RefreshTokenModel> ToModelList(this List<RefreshTokenEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}
