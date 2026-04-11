using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IWishlistItemRepository
    {
        public WishlistItemEntity? GetById(int id);
        public WishlistItemEntity? GetByWishlistAndProduct(int wishlistId, int productId);
        public List<WishlistItemEntity> GetByWishlistId(int wishlistId);
        public int Create(WishlistItemEntity entity);
        public bool Delete(int wishlistId, int productId);
    }
}