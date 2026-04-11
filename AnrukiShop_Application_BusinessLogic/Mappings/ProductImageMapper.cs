using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class ProductImageMapper
    {
        public static ProductImageModel ToModel(this ProductImageEntity entity)
        {
            return new ProductImageModel
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Url = entity.Url,
                IsPrimary = entity.IsPrimary
            };
        }

        public static List<ProductImageModel> ToModelList(List<ProductImageEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }      
    }
}
