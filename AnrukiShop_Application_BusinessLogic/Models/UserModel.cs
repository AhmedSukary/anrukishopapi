namespace AnrukiShop_Application.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
