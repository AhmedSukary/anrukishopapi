using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public ProductModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_NOT_FOUND", "Product not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public ProductSummaryModel GetSummaryById(int id)
        {
            var model = _repo.GetSummaryById(id)
                ?? throw new AppException("PRODUCT_NOT_FOUND", "Product not found");

            return model;
        }

        public List<ProductModel> GetAll()
        {
            return _repo.GetAll().ToModelList();
        }

        public List<ProductSummaryModel> GetProductsSummary()
        {
            return _repo.GetProductsSummary();
        }

        public List<ProductSummaryModel> GetProductsSummaryByCategoryId(int id)
        {
            return _repo.GetProductsSummaryByCategoryId(id);
        }

        public List<ProductSummaryModel> SearchProducts(string query)
        {
            return _repo.SearchProducts(query);
        }

        public List<ProductModel> GetByCategoryId(int id)
        {
            return _repo.GetByCategoryId(id).ToModelList();
        }

        public int Create(ProductModel model)
        {
            try
            {
                var entity = new ProductEntity(
                    model.Name,
                    model.Description,
                    model.Price,
                    model.SKU,
                    model.CategoryId
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool UpdateBasicInfo(int id, string name, string description, decimal price, int categoryId)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_NOT_FOUND", "Product not found");

                entity.ChangeName(name);
                entity.ChangeDescription(description);
                entity.ChangePrice(price);
                entity.ChangeCategory(categoryId);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Activate(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_NOT_FOUND", "Product not found");

                entity.Activate();

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Deactivate(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("PRODUCT_NOT_FOUND", "Product not found");

                entity.Deactivate();

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
                    ?? throw new AppException("PRODUCT_NOT_FOUND", "Product not found");

                entity.SoftDelete();

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}
