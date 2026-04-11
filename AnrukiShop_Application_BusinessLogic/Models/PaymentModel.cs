using AnrukiShop_Domain.Enums;

namespace AnrukiShop_Application.Models
{
    public class PaymentModel
    {
        public int Id { get;  set; }
        public int OrderId { get;  set; }
        public decimal Amount { get;  set; }
        public PaymentMethod Method { get;  set; }
        public PaymentStatus Status { get;  set; }
        public string? TransactionRef { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime? PaidAt { get;  set; }
    }
}