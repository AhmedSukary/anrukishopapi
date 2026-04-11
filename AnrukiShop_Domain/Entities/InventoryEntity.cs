using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class InventoryEntity
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public string Location { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public InventoryEntity(int productId, int quantity, string location)
        {
            if (productId <= 0)
                throw new DomainException("PRODUCT_REQUIRED", "Product is required");

            if (string.IsNullOrWhiteSpace(location))
                throw new DomainException("LOCATION_REQUIRED", "Inventory location is required");

            if (quantity < 0)
                throw new DomainException("QUANTITY_INVALID", "Quantity cannot be negative");

            ProductId = productId;
            Quantity = quantity;
            Location = location.Trim();
            LastUpdated = DateTime.UtcNow;
        }

        public void Increase(int amount)
        {
            if (amount <= 0)
                throw new DomainException("INCREASE_INVALID", "Increase amount must be greater than zero");

            Quantity += amount;
            Touch();
        }

        public void Decrease(int amount)
        {
            if (amount <= 0)
                throw new DomainException("DECREASE_INVALID", "Decrease amount must be greater than zero");

            if (Quantity - amount < 0)
                throw new DomainException("STOCK_NOT_ENOUGH", "Not enough stock available");

            Quantity -= amount;
            Touch();
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("QUANTITY_INVALID", "Quantity cannot be negative");

            Quantity = quantity;
            Touch();
        }

        public void ChangeLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new DomainException("LOCATION_REQUIRED", "Inventory location is required");

            Location = location.Trim();
            Touch();
        }

        private void Touch()
        {
            LastUpdated = DateTime.UtcNow;
        }

        internal InventoryEntity(
            int id,
            int productId,
            int quantity,
            string location,
            DateTime lastUpdated)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Location = location;
            LastUpdated = lastUpdated;
        }
    }
}
