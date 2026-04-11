using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class UserEntity
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName { get; private set; }
        public string Role { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Gender { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted {  get; private set; }

        public UserEntity(string email, string password, string fullName, string phoneNumber, string gender, DateTime dateOfBirth)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("EMAIL_REQUIRED", "Email is required");

            if (!email.Contains('@'))
                throw new DomainException("EMAIL_INVALID", "Email format is invalid");

            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("PASSWORD_REQUIRED", "Password is required");

            if (password.Length < 5)
                throw new DomainException("PASSWORD_WEAK", "Password is invalid");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("FULLNAME_REQUIRED", "Full name is required");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("PHONE_NUMBER_REQUIRED", "phone number is required");

            if (!IsValidGender(gender))
                throw new DomainException("GENDER_INVALID", "Invalid user gender");

            if (dateOfBirth > DateTime.Now.AddYears(-18) || dateOfBirth < DateTime.Now.AddYears(-100))
                throw new DomainException("AGE_INVALID", "Age must be between 18 and 100 years");

            Email = email;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Role = "User";
            IsDeleted = false;
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new DomainException("EMAIL_REQUIRED", "Email is required");

            if (!newEmail.Contains('@'))
                throw new DomainException("EMAIL_INVALID", "Email format is invalid");

            Email = newEmail;
        }

        public void ChangePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("PASSWORD_REQUIRED", "Password is required");

            if (password.Length < 5)
                throw new DomainException("PASSWORD_WEAK", "Password is invalid");

            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void ChangeRole(string newRole)
        {
            if (!IsValidRole(newRole))
                throw new DomainException("ROLE_INVALID", "Invalid user role");

            Role = newRole;
        }
        public void ChangeFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("FULLNAME_REQUIRED", "Full name is required");

          FullName = fullName;
        }
        public void ChangePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("PHONE_NUMBER_REQUIRED", "phone number is required");

            PhoneNumber = phoneNumber;
        }
        public void ChangeGender(string gender)
        {
            if (!IsValidGender(gender))
                throw new DomainException("GENDER_INVALID", "Invalid user gender");

            Gender = gender;
        }
        public void ChangeDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now.AddYears(-18) || dateOfBirth < DateTime.Now.AddYears(-100))
                throw new DomainException("AGE_INVALID", "Age must be between 18 and 100 years");

            DateOfBirth = dateOfBirth;
        }

        public void SoftDelete() => IsDeleted = true;

        private static bool IsValidRole(string role)
        {
            return role is "Admin" or "User";
        }

        private static bool IsValidGender(string gender)
        {
            return gender is "Male" or "Female";
        }

        internal UserEntity(
            int id,
            string email,
            string passwordHash,
            string fullName,
            string role,
            string phoneNumber,
            string gender,
            DateTime dateOfBirth,
            DateTime createdAt,
            bool isDeleted)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            Role = role;
            PhoneNumber = phoneNumber;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            CreatedAt = createdAt;
            IsDeleted = isDeleted;
        }
    }
}
