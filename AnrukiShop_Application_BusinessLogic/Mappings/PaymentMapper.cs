using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Mappings
{
    public static class PaymentMapper
    {
        public static PaymentModel ToModel(this PaymentEntity entity)
        {
            return new PaymentModel
            {
                Id = entity.Id,
                OrderId = entity.OrderId,
                Amount = entity.Amount,
                Method = entity.Method,
                Status = entity.Status,
                TransactionRef = entity.TransactionRef,
                CreatedAt = entity.CreatedAt,
                PaidAt = entity.PaidAt
            };
        }

        public static List<PaymentModel> ToModelList(List<PaymentEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}
