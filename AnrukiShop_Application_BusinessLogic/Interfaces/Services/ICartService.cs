using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface ICartService
    {
        public CartModel GetById(int id);
        public CartModel GetByUserId(int id);
        public List<CartModel> GetAll();
        public int AddItem(CartItemModel model);
        public bool RemoveItem(int itemId);
        public bool UpdateItemQuantity(int itemId, int quantity);
        public bool Clear(int id);
    }
}