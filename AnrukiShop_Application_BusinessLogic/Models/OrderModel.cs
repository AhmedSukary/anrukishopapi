using AnrukiShop_Domain.Enums;
namespace AnrukiShop_Application.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
}