using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Mappings
{
    public static class InventoryMapper
    {
        public static InventoryModel ToModel(this InventoryEntity entity)
        {
            return new InventoryModel
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                Location = entity.Location,
                LastUpdated = entity.LastUpdated
            };
        }

        public static List<InventoryModel> ToModelList(List<InventoryEntity> entities)
        {
            return entities.Select(ToModel).ToList();
        }
    }
}
