namespace AnrukiShop_API.Requests
{
    public class UpdateUserAddressRequest()
    {
        public required int Id { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string AddressLine { get; set; }
    }
    public class CreateUserAddressRequest
    {
        public required int UserId { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Region { get; set; }
        public required string AddressLine { get; set; }
        public required bool IsDefault { get; set; }
    }
    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Gender { get; set; }
        public required DateTime DateOfBirth { get; set; }
    }

    public class ChangePasswordRequest
    {
        public required string NewPassword { get; set; }
    }
    public class ChangeRoleRequest
    {
        public required string Role { get; set; }
    }

    public class UpdateProfileRequest
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Gender { get; set; }
        public required DateTime DateOfBirth { get; set; }
    }

    public class SendEmailCodeRequest
    {
        public required string Email { get; set; }
    }

    public class CheckEmailCodeRequest
    {
        public required string Code { get; set; }
    }

}
