using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class ProductEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string SKU { get; private set; }
        public int CategoryId { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }

        public ProductEntity(string name, string description, decimal price, string sku, int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("PRODUCT_NAME_REQUIRED", "Product name is required");

            if (name.Length < 3)
                throw new DomainException("PRODUCT_NAME_TOO_SHORT", "Product name must be at least 3 characters");

            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("PRODUCT_DESCRIPTION_REQUIRED", "Product description is required");

            if (price <= 0)
                throw new DomainException("PRICE_INVALID", "Product price must be greater than zero");

            if (string.IsNullOrWhiteSpace(sku))
                throw new DomainException("SKU_REQUIRED", "SKU is required");

            if (sku.Length < 4)
                throw new DomainException("SKU_INVALID", "SKU is invalid");

            if (categoryId <= 0)
                throw new DomainException("CATEGORY_REQUIRED", "Category is required");

            Name = name.Trim();
            Description = description.Trim();
            Price = price;
            SKU = sku.Trim().ToUpper();
            CategoryId = categoryId;
            IsActive = true;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("PRODUCT_NAME_REQUIRED", "Product name is required");

            if (name.Length < 3)
                throw new DomainException("PRODUCT_NAME_TOO_SHORT", "Product name must be at least 3 characters");

            Name = name.Trim();
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("PRODUCT_DESCRIPTION_REQUIRED", "Product description is required");

            Description = description.Trim();
        }

        public void ChangePrice(decimal price)
        {
            if (price <= 0)
                throw new DomainException("PRICE_INVALID", "Product price must be greater than zero");

            Price = price;
        }

        public void ChangeCategory(int categoryId)
        {
            if (categoryId <= 0)
                throw new DomainException("CATEGORY_REQUIRED", "Category is required");

            CategoryId = categoryId;
        }

        public void Activate() => IsActive = true;

        public void Deactivate() => IsActive = false;

        public void SoftDelete()
        {
            IsDeleted = true;
            IsActive = false;
        }

        internal ProductEntity(
            int id,
            string name,
            string description,
            decimal price,
            string sku,
            int categoryId,
            bool isActive,
            bool isDeleted,
            DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            SKU = sku;
            CategoryId = categoryId;
            IsActive = isActive;
            IsDeleted = isDeleted;
            CreatedAt = createdAt;
        }
    }
}
