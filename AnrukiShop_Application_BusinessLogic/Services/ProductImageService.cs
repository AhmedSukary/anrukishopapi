using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Exceptions;
using AnrukiShop_Domain.Entities;


namespace AnrukiShop_Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _repo;

        public ProductImageService(IProductImageRepository repo)
        {
            _repo = repo;
        }

        public ProductImageModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_IMAGE_NOT_FOUND", "Product image not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
        public List<ProductImageModel> GetProductImagesById(int id)
        {
            try
            {
                var entities = _repo.GetProductImagesById(id)
                   ?? throw new AppException("PRODUCT_IMAGE_NOT_FOUND", "Product image not found");

                return entities.Select(e => e.ToModel()).ToList();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public int Create(ProductImageModel model)
        {
            try
            {
                var entity = new ProductImageEntity(
                    model.ProductId,
                    model.Url,
                    model.IsPrimary
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Update(int id, string url, bool isPrimary)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_IMAGE_NOT_FOUND", "Product image not found");

                entity.Update(url, isPrimary);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_IMAGE_NOT_FOUND", "Product image not found");

                return _repo.Delete(id);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}
