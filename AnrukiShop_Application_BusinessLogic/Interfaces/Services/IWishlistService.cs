using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IWishlistService
    {
        public WishlistModel GetById(int id);
        public WishlistModel GetByUserId(int id);
        public List<WishlistModel> GetAll();
        public int AddItem(int wishlistId, int productId);
        public bool RemoveItem(int wishlistId, int productId);
    }
}