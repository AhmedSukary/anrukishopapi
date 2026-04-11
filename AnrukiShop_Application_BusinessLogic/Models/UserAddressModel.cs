namespace AnrukiShop_Application.Models
{
    public class UserAddressModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string AddressLine { get; set; }
        public bool IsDefault { get; set; }
    }
}
