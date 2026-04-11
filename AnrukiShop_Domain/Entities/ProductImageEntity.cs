using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class ProductImageEntity
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public string Url { get; private set; }
        public bool IsPrimary { get; private set; }

        public ProductImageEntity(int productId, string url, bool isPrimary)
        {
            if (productId <= 0)
                throw new DomainException("PRODUCT_ID_INVALID", "ProductId must be greater than zero");

            ValidateUrl(url);

            ProductId = productId;
            Url = url.Trim();
            IsPrimary = isPrimary;
        }

        public void Update(string url, bool isPrimary)
        {
            ValidateUrl(url);

            Url = url.Trim();
            IsPrimary = isPrimary;
        }

        private void ValidateUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new DomainException("IMAGE_URL_EMPTY", "Image URL is required");

            if (url.Length > 500)
                throw new DomainException("IMAGE_URL_TOO_LONG", "Image URL cannot exceed 500 characters");

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new DomainException("IMAGE_URL_INVALID_FORMAT", "Image URL must be a valid absolute URL");
        }

        internal ProductImageEntity(int id, int productId, string url, bool isPrimary)
        {
            Id = id;
            ProductId = productId;
            Url = url;
            IsPrimary = isPrimary;
        }
    }
}
