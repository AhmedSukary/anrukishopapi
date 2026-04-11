using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IOrderService
    {
        public OrderModel GetById(int id);
        public List<OrderModel> GetAll();
        public List<OrderModel> GetByUserId(int id);
        public void MarkAsShipped(int id);
        public void MarkAsCancelled(int id);
        public bool Delete(int id);
    }
}
