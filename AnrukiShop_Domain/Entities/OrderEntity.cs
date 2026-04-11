using AnrukiShop_Domain.Exceptions;
using AnrukiShop_Domain.Enums;
namespace AnrukiShop_Domain.Entities
{
    public class OrderEntity
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Total { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }

        public OrderEntity(int userId)
        {
            if (userId <= 0)
                throw new DomainException("USER_REQUIRED", "User is required");
         
            UserId = userId;
            Total = 0;
            Status = OrderStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void UpdateTotal(decimal total)
        {
            if (total <= 0)
                throw new DomainException("TOTAL_INVALID", "Order total must be greater than zero");

            Total = total;
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new DomainException("ID_INVALID", "Invalid order id");

            Id = id;
        }

        public bool IsPaid() => Status == OrderStatus.Paid;
        public void MarkAsPaid() => Status = OrderStatus.Paid;
        public void MarkAsShipped() => Status = OrderStatus.Shipped;
        public void MarkAsCancelled() => Status = OrderStatus.Cancelled;
        public void SoftDelete() => IsDeleted = true;

        internal OrderEntity(
            int id,
            int userId,
            OrderStatus status,
            decimal total,
            DateTime createdAt,
            bool isDeleted
        )
        {
            Id = id;
            UserId = userId;
            Status = status;
            Total = total;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
        }
    }
}
