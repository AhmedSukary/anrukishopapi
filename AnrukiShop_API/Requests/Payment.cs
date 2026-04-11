using AnrukiShop_Domain.Enums;

namespace AnrukiShop_API.Requests
{
    public class CreatePaymentRequest
    {
        public required int OrderId { get; set; }
        public required PaymentMethod Method { get; set; }
    }

    public class PayRequest
    {
        public required int PaymentId { get; set; }
        public required string TransactionRef { get; set; }
    }
}
