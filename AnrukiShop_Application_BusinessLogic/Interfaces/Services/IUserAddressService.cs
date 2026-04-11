
using AnrukiShop_Application.Models;
namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IUserAddressService
    {
        public UserAddressModel GetById(int id);
        public UserAddressModel GetDefaultAddressByUserId(int id);
        public List<UserAddressModel> GetByUser(int userId);
        public int Create(UserAddressModel model);
        public bool Update(int id, string country, string city, string region, string addressLine);
        public bool Delete(int id);
    }
}
