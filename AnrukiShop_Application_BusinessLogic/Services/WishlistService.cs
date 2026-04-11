using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistItemRepository _wishlistItemRepository;

        public WishlistService(IWishlistRepository wishlistRepository, IWishlistItemRepository wishlistItemRepository)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistItemRepository = wishlistItemRepository;
        }

        public WishlistModel GetById(int id)
        {
            try
            {
                var wishlistEntity = _wishlistRepository.GetById(id)
                    ?? throw new AppException("WISHLIST_NOT_FOUND", "Wishlist not found");

                var wishlistItemEntityList = _wishlistItemRepository.GetByWishlistId(id);

                var model = wishlistEntity.ToModel();

                model.Items = wishlistItemEntityList.ToModelList();

                return model;
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public WishlistModel GetByUserId(int id)
        {
            try
            {
                var wishlistEntity = _wishlistRepository.GetByUserId(id)
                    ?? throw new AppException("WISHLIST_NOT_FOUND", "Wishlist not found");

                var wishlistItemEntityList = _wishlistItemRepository.GetByWishlistId(wishlistEntity.Id);

                var model = wishlistEntity.ToModel();

                model.Items = wishlistItemEntityList.ToModelList();

                return model;
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<WishlistModel> GetAll()
        {
            return _wishlistRepository.GetAll().ToModelList();
        }

        public int AddItem(int wishlistId, int productId)
        {
            try
            {
                var existing = _wishlistItemRepository.GetByWishlistAndProduct(wishlistId, productId);

                if (existing is not null)
                    return existing.Id;

                var entity = new WishlistItemEntity(wishlistId, productId);

                return _wishlistItemRepository.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool RemoveItem(int wishlistId, int productId)
        {
            try
            {
                return _wishlistItemRepository.Delete(wishlistId, productId);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}