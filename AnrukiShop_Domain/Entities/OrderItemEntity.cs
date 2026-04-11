using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public OrderItemEntity(int orderId, int productId, int quantity, decimal unitPrice)
        {
            if (orderId <= 0)
                throw new DomainException("ORDER_REQUIRED", "Order is required");

            if (productId <= 0)
                throw new DomainException("PRODUCT_REQUIRED", "Product is required");

            if (quantity <= 0)
                throw new DomainException("QUANTITY_INVALID", "Quantity must be greater than zero");

            if (unitPrice <= 0)
                throw new DomainException("PRICE_INVALID", "Unit price must be greater than zero");

            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal OrderItemEntity(
            int id,
            int orderId,
            int productId,
            int quantity,
            decimal unitPrice)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}