using AnrukiShop_Domain.Entities;
using AnrukiShop_Application.Models;

namespace AnrukiShop_Application.Mappings
{
    public static class UserAddressMapper
    {
        public static UserAddressModel ToModel(this UserAddressEntity entity)
        {
            return new UserAddressModel
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Country = entity.Country,
                City = entity.City,
                Region = entity.Region,
                AddressLine = entity.AddressLine,
                IsDefault = entity.IsDefault
            };
        }
        public static List<UserAddressModel> ToModelList(this IEnumerable<UserAddressEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}