using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        public ProductEntity? GetById(int id);
        public ProductSummaryModel? GetSummaryById(int id);
        public List<ProductEntity> GetAll();
        public List<ProductEntity> GetByCategoryId(int id);
        public int Create(ProductEntity entity);
        public bool Update(ProductEntity entity);
        public bool Delete(int id);
        public List<ProductSummaryModel> GetProductsSummary();
        public List<ProductSummaryModel> GetProductsSummaryByCategoryId(int id);
        public List<ProductSummaryModel> SearchProducts(string query);
    }
}
