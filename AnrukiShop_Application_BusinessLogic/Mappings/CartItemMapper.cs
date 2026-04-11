using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class CartItemMapper
    {
        public static CartItemModel ToModel(this CartItemEntity entity)
        {
            return new CartItemModel
            {
                Id = entity.Id,
                CartId = entity.CartId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                Price = entity.Price
            };
        }

        public static List<CartItemModel> ToModelList(this List<CartItemEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}