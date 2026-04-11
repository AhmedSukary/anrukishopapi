using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        public OrderEntity? GetById(int id);
        public List<OrderEntity> GetAll();
        public List<OrderEntity> GetByUserId(int id);
        public List<OrderItemEntity> GetItemsByOrderId(int orderId);
        public bool Update(OrderEntity entity);
    }
}
