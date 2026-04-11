using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        public CartEntity? GetById(int id);
        public CartEntity? GetByUserId(int id);
        public List<CartEntity> GetAll();
        public int Create(CartEntity entity);
        public bool Delete(int id);
        public bool Clear(int id);
    }
}