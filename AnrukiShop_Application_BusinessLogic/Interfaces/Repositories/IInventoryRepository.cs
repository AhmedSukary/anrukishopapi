using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IInventoryRepository
    {
        public InventoryEntity? GetByProductId(int id);
        public InventoryEntity? GetById(int id);
        public List<InventoryEntity> GetAll();
        public int Create(InventoryEntity entity);
        public bool Update(InventoryEntity entity);
        public bool Delete(int id);
    }
}