using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IProductImageService
    {
        public ProductImageModel GetById(int id);
        public List<ProductImageModel> GetProductImagesById(int id);
        public int Create(ProductImageModel image);
        public bool Update(int id, string url, bool isPrimary);
        public bool Delete(int id);
    }
}