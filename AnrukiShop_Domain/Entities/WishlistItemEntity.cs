using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class WishlistItemEntity
    {
        public int Id { get; private set; }
        public int WishlistId { get; private set; }
        public int ProductId { get; private set; }

        public WishlistItemEntity(int wishlistId, int productId)
        {
            if (wishlistId <= 0)
                throw new DomainException("WISHLIST_REQUIRED", "Wishlist is required");

            if (productId <= 0)
                throw new DomainException("PRODUCT_REQUIRED", "Product is required");

            WishlistId = wishlistId;
            ProductId = productId;
        }

        internal WishlistItemEntity(int id, int wishlistId, int productId)
        {
            Id = id;
            WishlistId = wishlistId;
            ProductId = productId;
        }
    }
}