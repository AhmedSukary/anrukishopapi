using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class CategoryMapper
    {
        public static CategoryModel ToModel(this CategoryEntity entity)
        {
            return new CategoryModel
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentCategoryId = entity.ParentCategoryId,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted
            };
        }

        public static List<CategoryModel> ToModelList(this List<CategoryEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}
