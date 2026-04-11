using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IWishlistRepository
    {
        public WishlistEntity? GetById(int id);
        public WishlistEntity? GetByUserId(int id);
        public List<WishlistEntity> GetAll();
        public int Create(WishlistEntity entity);
    }
}