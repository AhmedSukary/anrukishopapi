using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class WishlistItemMapper
    {
        public static WishlistItemModel ToModel(this WishlistItemEntity entity)
        {
            return new WishlistItemModel
            {
                Id = entity.Id,
                WishlistId = entity.WishlistId,
                ProductId = entity.ProductId
            };
        }

        public static List<WishlistItemModel> ToModelList(this List<WishlistItemEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}