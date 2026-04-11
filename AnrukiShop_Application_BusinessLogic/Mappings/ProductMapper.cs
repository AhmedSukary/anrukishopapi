using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class ProductMapper
    {
        public static ProductModel ToModel(this ProductEntity entity)
        {
            return new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                SKU = entity.SKU,
                CategoryId = entity.CategoryId,
                IsActive = entity.IsActive,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted
            };
        }

        public static List<ProductModel> ToModelList(this IEnumerable<ProductEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}
