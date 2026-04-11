using AnrukiShop_Domain.Exceptions;
using AnrukiShop_Domain.Enums;

namespace AnrukiShop_Domain.Entities
{
    public class PaymentEntity
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string? TransactionRef { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }

        public PaymentEntity(int orderId, decimal amount, PaymentMethod method)
        {
            if (orderId <= 0)
                throw new DomainException("ORDER_ID_INVALID", "Order id must be greater than zero.");

            if (amount <= 0)
                throw new DomainException("AMOUNT_INVALID", "Payment amount must be greater than zero.");

            if (!Enum.IsDefined(typeof(PaymentMethod), method))
                throw new DomainException("PAYMENT_METHOD_INVALID", "Payment method is not valid.");

            OrderId = orderId;
            Amount = amount;
            Method = method;

            Status = PaymentStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsPaid(string transactionRef)
        {
            if (Status == PaymentStatus.Paid)
                throw new DomainException("PAYMENT_ALREADY_PAID", "Payment is already completed.");

            if (string.IsNullOrWhiteSpace(transactionRef))
                throw new DomainException("TRANSACTION_REF_REQUIRED", "Transaction reference is required.");

            Status = PaymentStatus.Paid;
            TransactionRef = transactionRef.Trim();
            PaidAt = DateTime.UtcNow;
        }

        public void MarkAsFailed()
        {
            if (Status == PaymentStatus.Paid)
                throw new DomainException("PAID_PAYMENT_CANNOT_FAIL", "A completed payment cannot be marked as failed.");

            Status = PaymentStatus.Failed;
        }

        public void Refund()
        {
            if (Status != PaymentStatus.Paid)
                throw new DomainException("REFUND_NOT_ALLOWED", "Only paid payments can be refunded.");

            Status = PaymentStatus.Refunded;
        }

        internal PaymentEntity(
            int id,
            int orderId,
            decimal amount,
            PaymentMethod method,
            PaymentStatus status,
            string? transactionRef,
            DateTime createdAt,
            DateTime? paidAt)
        {
            Id = id;
            OrderId = orderId;
            Amount = amount;
            Method = method;
            Status = status;
            TransactionRef = transactionRef;
            CreatedAt = createdAt;
            PaidAt = paidAt;
        }
    }
}