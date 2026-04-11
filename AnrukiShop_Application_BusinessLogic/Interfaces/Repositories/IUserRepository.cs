using AnrukiShop_Domain.Entities;
namespace AnrukiShop_Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public List<UserEntity> GetAllUsers();
        public UserEntity? GetById(int id);
        public UserEntity? GetByEmail(string email);
        public int Create(UserEntity entity);
        public bool Update(UserEntity entity);
        public bool Delete(int id);
        public bool GetEmailVerificationCode(string code);
        public bool AddEmailVerificationCode(string code);
        public bool DeleteEmailVerificationCode(string code);
    }
}