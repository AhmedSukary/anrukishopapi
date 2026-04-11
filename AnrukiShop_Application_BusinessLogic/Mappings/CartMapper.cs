using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class CartMapper
    {
        public static CartModel ToModel(this CartEntity entity)
        {
            return new CartModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                CreatedAt = entity.CreatedAt
            };
        }

        public static List<CartModel> ToModelList(this List<CartEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}