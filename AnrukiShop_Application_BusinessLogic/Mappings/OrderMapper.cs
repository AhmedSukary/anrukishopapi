using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class OrderMapper
    {
        public static OrderModel ToModel(this OrderEntity entity)
        {
            return new OrderModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Status = entity.Status,
                Total = entity.Total,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted
            };
        }

        public static List<OrderModel> ToModelList(this List<OrderEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}
