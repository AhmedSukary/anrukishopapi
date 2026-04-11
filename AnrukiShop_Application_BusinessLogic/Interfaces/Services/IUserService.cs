using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;

namespace AnrukiShop_Application.Interfaces.Services
{
    public interface IUserService
    {
        public List<UserModel> GetAllUsers();
        public UserModel GetById(int id);
        public UserModel GetByEmail(string email);
        public int Create(UserModel user);
        public bool UpdateProfile(int id, string fullName, string phoneNumber, string gender, DateTime dateOfBirth);
        public bool ChangeRole(int id, string role);
        public bool ChangePassword(int userId, string password);
        public bool Delete(int id);
        public Task<bool> SendEmailVerificationCode(string email);
        public bool CheckEmailVerificationCode(string code);
        public bool DeleteEmailVerificationCode(string code);
    }
}