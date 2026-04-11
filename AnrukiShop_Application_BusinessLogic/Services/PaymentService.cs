using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Exceptions;
using AnrukiShop_Domain.Enums;

namespace AnrukiShop_Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IEmailService _emailService;

        public PaymentService( IUserRepository userRepo, IPaymentRepository paymentRepo, IEmailService emailService)
        {
            _userRepo = userRepo;
            _paymentRepo = paymentRepo;
            _emailService = emailService;
        }

        public int Create(int userId, int orderId, PaymentMethod method)
        {
            try
            {
                return _paymentRepo.Create(userId, orderId, method);
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Pay(int userId, int paymentId, string transactionRef)
        {
            try
            {
                var payment = _paymentRepo.GetById(paymentId)
                    ?? throw new AppException("PAYMENT_NOT_FOUND", "Payment not found");

                if (_paymentRepo.Pay(paymentId, transactionRef))
                {
                    var user = _userRepo.GetById(userId);
                    var subject = $"Order #{payment.OrderId} Payment Successful";
                    var body = $@"<div style='font-family:Arial;background:#f4f4f4;padding:20px;'>
                                <div style='background:white;padding:20px;border-radius:8px;'>
                                    <h1><a style='color:#4caf50;' href='https://ahmedsukary.github.io/anrukishop/'>AnrukiShop</a></h1>
                                    <h2>Payment Successful</h2>
                                    <p>Hello <strong>{user.FullName}</strong>,</p>
                                    <p> Your order <strong>#{payment.OrderId}</strong> has been paid successfully. </p>
                                    <p><strong>Amount:</strong> {payment.Amount} USD</p>
                                    <hr />
                                    <p style='font-size:12px;color:gray;'> Thank you for shopping with AnrukiShop 💚 </p>
                                </div>
                              </div> ";
                    _ = _emailService.SendAsync(user.Email, subject, body);
                }

                return true;
            }
            catch (AppException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public PaymentModel? GetById(int id)
        {
            try
            {
                var entity = _paymentRepo.GetById(id)
                    ?? throw new AppException("PAYMENT_NOT_FOUND", "Payment not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public PaymentModel? GetByOrderId(int orderId)
        {
            try
            {
                var entity = _paymentRepo.GetByOrderId(orderId)
                    ?? throw new AppException("PAYMENT_NOT_FOUND", "Payment not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}