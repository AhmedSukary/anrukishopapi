using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface ICartItemRepository
    {
        public CartItemEntity? GetById(int id);
        CartItemEntity? GetByCartAndProduct(int cartId, int productId);
        public List<CartItemEntity> GetByCartId(int id);
        public int Create(CartItemEntity entity);
        public bool Update(CartItemEntity entity);
        public bool Delete(int id);
    }
}