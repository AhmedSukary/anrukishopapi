using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class CartItemEntity
    {
        public int Id { get; private set; }
        public int CartId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public CartItemEntity(int cartId, int productId, int quantity, decimal price)
        {
            if (cartId <= 0)
                throw new DomainException("CART_REQUIRED", "Cart is required");

            if (productId <= 0)
                throw new DomainException("PRODUCT_REQUIRED", "Product is required");

            if (quantity <= 0)
                throw new DomainException("QUANTITY_INVALID", "Quantity must be greater than zero");

            if (price <= 0)
                throw new DomainException("PRICE_INVALID", "Price must be greater than zero");

            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public void ChangeQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("QUANTITY_INVALID", "Quantity must be greater than zero");

            Quantity = quantity;
        }

        internal CartItemEntity(
            int id,
            int cartId,
            int productId,
            int quantity,
            decimal price)
        {
            Id = id;
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }
}