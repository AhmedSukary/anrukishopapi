using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Application.Mappings;

namespace AnrukiShop_Application.Services
{
    public class UserAddressService : IUserAddressService
    {
        private readonly IUserAddressRepository _repo;

        public UserAddressService(IUserAddressRepository repo)
        {
            _repo = repo;
        }

        public UserAddressModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("USER_ADDRESS_NOT_FOUND", "User address not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public List<UserAddressModel> GetByUser(int userId)
        {
            try
            {
                var entites = _repo.GetByUser(userId);

                if (entites.Count == 0)
                    throw new AppException("USER_ADDRESS_NOT_FOUND", "User address not found");

                return entites.ToModelList();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public UserAddressModel GetDefaultAddressByUserId(int id)
        {
            try
            {
                var entity = _repo.GetDefaultAddressByUserId(id)
                    ?? throw new AppException("USER_ADDRESS_NOT_FOUND", "User address not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public int Create(UserAddressModel model)
        {
            try
            {
                var entity = new UserAddressEntity(
                    model.UserId,
                    model.Country,
                    model.City,
                    model.Region,
                    model.AddressLine,
                    model.IsDefault
                );

                return _repo.Create(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool Update(int id, string country, string city, string region, string addressLine)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("USER_ADDRESS_NOT_FOUND", "User address not found");

                entity.UpdateAddress(country, city, region, addressLine);

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
                    ?? throw new AppException("USER_ADDRESS_NOT_FOUND", "User address not found");

                return _repo.Delete(id);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}