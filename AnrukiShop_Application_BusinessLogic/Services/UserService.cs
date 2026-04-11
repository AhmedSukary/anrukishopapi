using AnrukiShop_Application.Interfaces.Repositories;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Domain.Entities;
using AnrukiShop_Domain.Exceptions;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_Application.Mappings;

namespace AnrukiShop_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly ICartRepository _cartRepo;
        private readonly IWishlistRepository _wishlistRepo;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository repo, ICartRepository cartRepo, IWishlistRepository wishlistRepo, IEmailService emailService)
        {
            _repo = repo;
            _cartRepo = cartRepo;
            _wishlistRepo = wishlistRepo;
            _emailService = emailService;
        }

        public List<UserModel> GetAllUsers()
        {
            return _repo.GetAllUsers().ToModelList();
        }

        public UserModel GetById(int id)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public UserModel GetByEmail(string email)
        {
            try
            {
                var entity = _repo.GetByEmail(email)
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                return entity.ToModel();
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public async Task<bool> SendEmailVerificationCode(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new AppException("EMAIL_REQUIRED", "Email is required");

            if (!email.Contains('@'))
                throw new AppException("EMAIL_INVALID", "Email format is invalid");

            if (_repo.GetByEmail(email) != null)
                throw new AppException("USER_EXIST", "User already exist");

            int number = Random.Shared.Next(100000, 1000000);

            if (!_repo.AddEmailVerificationCode(number.ToString()))
                throw new AppException("CODE_NOT_ADDED", "Verification code not added");

            string subject = "Verification Code";
            string body = $@"<div style='max-width:500px; margin:40px auto; background:#f9f9f9; padding:25px; border-radius:10px; font-family:Arial, sans-serif; text-align:center;'>
                                <h1><a style='color:#4caf50;' href='https://ahmedsukary.github.io/anrukishop/'>AnrukiShop</a></h1>
                                <h2 style='color:#333;'>Email Verification</h2>
                                <p>Please use the verification code below:</p>
                                <div style='margin:20px 0;'>
                                    <span style='padding:12px 25px; font-size:24px; font-weight:bold; letter-spacing:4px; background:#4caf50; color:#fff; border-radius:6px;'>
                                        {number}
                                    </span>
                                </div>
                                <p style='font-size:13px;'>This code will expire in 5 minutes.</p>
                             </div>";

            try
            {
                await _emailService.SendAsync(email, subject, body);
                return true;
            }
            catch (Exception ex)
            {
                throw new AppException("ERROR", ex.Message);
            }
        }

        public bool CheckEmailVerificationCode(string code)
        {
            return _repo.GetEmailVerificationCode(code);
        }

        public bool DeleteEmailVerificationCode(string code)
        {
            return _repo.DeleteEmailVerificationCode(code);
        }

        public int Create(UserModel model)
        {
            try
            {
                if (_repo.GetByEmail(model.Email) != null)
                    throw new AppException("USER_EXIST", "User already exist");

                var entity = new UserEntity(
                    model.Email,
                    model.Password,
                    model.FullName,
                    model.PhoneNumber,
                    model.Gender,
                    model.DateOfBirth
                );

                var newId = _repo.Create(entity);

                var cartEntity = new CartEntity(newId);

                var wishlistEntity = new WishlistEntity(newId);

                _cartRepo.Create(cartEntity);

                _wishlistRepo.Create(wishlistEntity);

                return newId;
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool UpdateProfile(int id, string fullName, string phoneNumber, string gender, DateTime dateOfBirth)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                entity.ChangeFullName(fullName);
                entity.ChangePhoneNumber(phoneNumber);
                entity.ChangeGender(gender);
                entity.ChangeDateOfBirth(dateOfBirth);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool ChangePassword(int userId, string password)
        {
            try
            {
                var entity = _repo.GetById(userId)
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                entity.ChangePassword(password);

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }

        public bool ChangeRole(int id, string role)
        {
            try
            {
                var entity = _repo.GetById(id)
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                entity.ChangeRole(role);

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
                    ?? throw new AppException("USER_NOT_FOUND", "User not found");

                entity.SoftDelete();

                return _repo.Update(entity);
            }
            catch (DomainException ex)
            {
                throw new AppException(ex.Code, ex.Message);
            }
        }
    }
}