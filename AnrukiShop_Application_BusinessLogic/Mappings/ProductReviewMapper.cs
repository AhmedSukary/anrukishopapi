using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class ProductReviewMapper
    {
        public static ProductReviewModel ToModel(this ProductReviewEntity entity)
        {
            return new ProductReviewModel
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                UserName = entity.UserNmae,
                Rating = entity.Rating,
                Comment = entity.Comment,
                CreatedAt = entity.CreatedAt
            };
        }

        public static List<ProductReviewModel> ToModelList(this List<ProductReviewEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}