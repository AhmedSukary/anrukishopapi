using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Enums;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IPaymentService
    {
        public PaymentModel? GetByOrderId(int orderId);
        public PaymentModel? GetById(int id);
        public int Create(int userId, int orderId, PaymentMethod method);
        public bool Pay(int userId, int paymentId, string transactionRef);
    }
}