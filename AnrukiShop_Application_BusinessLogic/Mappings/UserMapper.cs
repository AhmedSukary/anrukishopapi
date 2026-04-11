using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Mappings
{
    public static class UserMapper
    {
        public static UserModel ToModel(this UserEntity entity)
        {
            return new UserModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = entity.PasswordHash,
                FullName = entity.FullName,
                Role = entity.Role,
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                DateOfBirth = entity.DateOfBirth,
                CreatedAt = entity.CreatedAt,
                IsDeleted = entity.IsDeleted,
            };
        }
        public static List<UserModel> ToModelList(this IEnumerable<UserEntity> entities)
        {
            return entities.Select(e => e.ToModel()).ToList();
        }
    }
}