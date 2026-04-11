using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Enums;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        public PaymentEntity? GetByOrderId(int orderId);
        public PaymentEntity? GetById(int id);
        public int Create(int userId, int orderId, PaymentMethod method); 
        public bool Pay(int id, string transactionRef);
    }
}