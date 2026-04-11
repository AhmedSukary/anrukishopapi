using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IProductImageRepository
    {
        public ProductImageEntity? GetById(int id);
        public List<ProductImageEntity> GetProductImagesById(int id);
        public int Create(ProductImageEntity entity);
        public bool Update(ProductImageEntity entity);
        public bool Delete(int id);
    }
}
