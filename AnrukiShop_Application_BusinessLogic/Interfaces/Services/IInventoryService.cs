using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IInventoryService
    {
        public InventoryModel GetById(int id);
        public InventoryModel GetByProductId(int id);
        public List<InventoryModel> GetAll();
        public int Create(InventoryModel inventory);
        public bool IncreaseStock(int id, int amount);
        public bool DecreaseStock(int id, int amount);
        public bool ChangeLocation(int id, string location);
        public bool SetQuantity(int id, int quantity);
        public bool Delete(int id);
    }
}