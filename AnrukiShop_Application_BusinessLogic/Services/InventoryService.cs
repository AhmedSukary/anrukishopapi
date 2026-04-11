using AnrukiShop_Application.Exceptions;
using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Mappings;
using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repo;

        public InventoryService(IInventoryRepository repo)
        {
            _repo = repo;
        }

        public InventoryModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public InventoryModel GetByProductId(int id)
        {
            try
            {
                var entity = _repo.GetByProductId(id)
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<InventoryModel> GetAll()
        {
            return _repo.GetAll().Select(e => e.ToModel()).ToList();
        }

        public int Create(InventoryModel model)
        {
            try
            {
                var entity = new InventoryEntity(
                    model.ProductId,
                    model.Quantity,
                    model.Location
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool IncreaseStock(int id, int amount)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                entity.Increase(amount);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool DecreaseStock(int id, int amount)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                entity.Decrease(amount);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool ChangeLocation(int id, string location)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                entity.ChangeLocation(location);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool SetQuantity(int id, int quantity)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                entity.SetQuantity(quantity);

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
                    ?? throw new AppException("INVENTORY_NOT_FOUND", "Invevtory not found");

                return _repo.Delete(id);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}
