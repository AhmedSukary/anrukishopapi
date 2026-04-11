using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _repo;

        public ProductReviewService(IProductReviewRepository repo)
        {
            _repo = repo;
        }

        public ProductReviewModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("REVIEW_NOT_FOUND", "Review not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<ProductReviewModel> GetByProductId(int productId)
        {
            return _repo.GetByProductId(productId).ToModelList();
        }

        public int Create(ProductReviewModel model)
        {
            try
            {
                var entity = new ProductReviewEntity(
                    model.ProductId,
                    model.UserName,
                    model.Rating,
                    model.Comment
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Update(int id, int rating, string comment)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("REVIEW_NOT_FOUND", "Review not found");

                entity.ChangeRating(rating);
                entity.ChangeComment(comment);

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
                    ?? throw new AppException("REVIEW_NOT_FOUND", "Review not found");

                return _repo.Delete(id);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}