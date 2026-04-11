using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IProductReviewService
    {
        public ProductReviewModel GetById(int id);
        public List<ProductReviewModel> GetByProductId(int id);
        public int Create(ProductReviewModel model);
        public bool Update(int id, int rating, string comment);
        public bool Delete(int id);
    }
}