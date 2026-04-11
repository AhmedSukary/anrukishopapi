using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IProductService
    {
        ProductModel GetById(int id);
        public ProductSummaryModel GetSummaryById(int id);
        public List<ProductModel> GetAll();
        public List<ProductSummaryModel> GetProductsSummary();
        public List<ProductSummaryModel> GetProductsSummaryByCategoryId(int id);
        public List<ProductSummaryModel> SearchProducts(string query);
        public List<ProductModel> GetByCategoryId(int id);
        public int Create(ProductModel product);
        public bool UpdateBasicInfo(int id, string name, string description, decimal price, int categoryId);
        public bool Activate(int id);
        public bool Deactivate(int id);
        public bool Delete(int id);
    }
}
