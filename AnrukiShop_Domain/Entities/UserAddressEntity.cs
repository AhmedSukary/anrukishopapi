using System.Diagnostics.Metrics;
using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class UserAddressEntity
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Country { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string AddressLine { get; private set; }
        public bool IsDefault { get; private set; }

        public UserAddressEntity(
            int userId,
            string country,
            string city,
            string region,
            string addressLine,
            bool isDefault)
        {
            if (userId <= 0)
                throw new DomainException("USER_REQUIRED", "User is required");

            if (string.IsNullOrWhiteSpace(country))
                throw new DomainException("COUNTRY_REQUIRED", "Country is required");

            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("CITY_REQUIRED", "City is required");

            if (string.IsNullOrWhiteSpace(region))
                throw new DomainException("REGION_REQUIRED", "Region is required");

            if (string.IsNullOrWhiteSpace(addressLine))
                throw new DomainException("ADDRESS_LINE_REQUIRED", "Address line is required");

            UserId = userId;
            Country = country;
            City = city;
            Region = region;
            AddressLine = addressLine;
            IsDefault = isDefault;
        }

        public void SetDefault(bool value)
        {
            IsDefault = value;
        }
        
        public void UpdateAddress(string country, string city, string region, string addressLine)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new DomainException("COUNTRY_REQUIRED", "Country is required");

            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("CITY_REQUIRED", "City is required");

            if (string.IsNullOrWhiteSpace(region))
                throw new DomainException("REGION_REQUIRED", "Region is required");

            if (string.IsNullOrWhiteSpace(addressLine))
                throw new DomainException("ADDRESS_REQUIRED", "Address is required");

            Country = country;
            City = city;
            Region = region;
            AddressLine = addressLine;
        }

        internal UserAddressEntity(
            int id,
            int userId,
            string country,
            string city,
            string region,
            string addressLine,
            bool isDefault)
        {
            Id = id;
            UserId = userId;
            Country = country;
            City = city;
            Region = region;
            AddressLine = addressLine;
            IsDefault = isDefault;
        }
    }
}

