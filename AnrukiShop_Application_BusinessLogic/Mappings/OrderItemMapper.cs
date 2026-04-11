using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class OrderItemMapper
    {
        public static OrderItemModel ToModel(this OrderItemEntity entity)
        {
            return new OrderItemModel
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                UnitPrice = entity.UnitPrice
            };
        }

        public static List<OrderItemModel> ToModelList(this List<OrderItemEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}