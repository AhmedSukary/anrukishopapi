using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public CartModel GetById(int id)
        {
            try
            {
                var cartEntity = _cartRepository.GetById(id)
                    ?? throw new AppException("CART_NOT_FOUND", "Cart not found");

                var cartItemEntityList = _cartItemRepository.GetByCartId(id);

                var cartModel = cartEntity.ToModel();

                cartModel.Items = cartItemEntityList.ToModelList();

                return cartModel;
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public CartModel GetByUserId(int id)
        {
            try
            {
                var cartEntity = _cartRepository.GetByUserId(id)
                    ?? throw new AppException("CART_NOT_FOUND", "Cart not found");

                var cartItemEntityList = _cartItemRepository.GetByCartId(cartEntity.Id);

                var cartModel = cartEntity.ToModel();

                cartModel.Items = cartItemEntityList.ToModelList();

                return cartModel;
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<CartModel> GetAll()
        {
            return _cartRepository.GetAll().ToModelList();
        }

        public bool Clear(int id)
        {
            try
            {
                var entity = _cartRepository.GetById(id)
                    ?? throw new AppException("CART_NOT_FOUND", "Cart not found");

                return _cartRepository.Clear(id);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public int AddItem(CartItemModel model)
        {
            try
            {
                var existing = _cartItemRepository.GetByCartAndProduct(model.CartId, model.ProductId);

                if (existing is not null)
                {
                    existing.ChangeQuantity(existing.Quantity + model.Quantity);

                    _cartItemRepository.Update(existing);

                    return existing.Id;
                }

                var entity = new CartItemEntity(
                    model.CartId,
                    model.ProductId,
                    model.Quantity,
                    model.Price
                );

                return _cartItemRepository.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool RemoveItem(int itemId)
        {
            try
            {
                var entity = _cartItemRepository.GetById(itemId)
                    ?? throw new DomainException("CART_ITEM_NOT_FOUND", "Cart item not found");

                return _cartItemRepository.Delete(itemId);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool UpdateItemQuantity(int itemId, int quantity)
        {
            try
            {
                var entity = _cartItemRepository.GetById(itemId)
                    ?? throw new DomainException("CART_ITEM_NOT_FOUND", "Cart item not found");

                entity.ChangeQuantity(quantity);
                return _cartItemRepository.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}