using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IProductReviewRepository
    {
        public ProductReviewEntity? GetById(int id);
        public List<ProductReviewEntity> GetByProductId(int id);
        public int Create(ProductReviewEntity entity);
        public bool Update(ProductReviewEntity entity);
        public bool Delete(int id);
    }
}