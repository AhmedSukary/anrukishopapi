using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IUserAddressRepository
    {
        public UserAddressEntity? GetDefaultAddressByUserId(int userId);
        public List<UserAddressEntity> GetByUser(int userId);
        public UserAddressEntity? GetById(int Id);
        public int Create(UserAddressEntity entity);
        public bool Update(UserAddressEntity entity);
        public bool Delete(int id);
    }
}
