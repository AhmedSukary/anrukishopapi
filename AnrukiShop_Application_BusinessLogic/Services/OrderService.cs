using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public OrderModel GetById(int id)
        {
            try
            {
                var orderEntity = _repo.GetById(id)
                    ?? throw new AppException("ORDER_NOT_FOUND", "Order not found");

                var orderItemEntityList = _repo.GetItemsByOrderId(orderEntity.Id);

                var orderModel = orderEntity.ToModel();

                orderModel.Items = orderItemEntityList.ToModelList();

                return orderModel;
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<OrderModel> GetAll()
        {
            try
            {
                return _repo.GetAll().ToModelList();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<OrderModel> GetByUserId(int id)
        {
            try
            {
                return _repo.GetByUserId(id).ToModelList();
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
                    ?? throw new AppException("ORDER_NOT_FOUND", "Order not found");

                entity.SoftDelete();

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public void MarkAsShipped(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("ORDER_NOT_FOUND", "Order not found");

                entity.MarkAsShipped();

                _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public void MarkAsCancelled(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("ORDER_NOT_FOUND", "Order not found");

                entity.MarkAsCancelled();

                _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}
