using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class WishlistMapper
    {
        public static WishlistModel ToModel(this WishlistEntity entity)
        {
            return new WishlistModel
            {
                Id = entity.Id,
                UserId = entity.UserId
            };
        }

        public static List<WishlistModel> ToModelList(this List<WishlistEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}