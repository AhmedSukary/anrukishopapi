using AnrukiShop_Domain.Exceptions;

namespace AnrukiShop_Domain.Entities
{
    public class ProductReviewEntity
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public string UserNmae { get; private set; }
        public int? Rating { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ProductReviewEntity(int productId, string userName, int? rating, string? comment)
        {
            if (productId <= 0)
                throw new DomainException("PRODUCT_REQUIRED", "Product is required");

            if (string.IsNullOrWhiteSpace(userName))
                throw new DomainException("USER_REQUIRED", "User is required");

            if (rating == null && string.IsNullOrWhiteSpace(comment))
                throw new DomainException("REVIEW_EMPTY", "Review must contain rating or comment");

            ValidateRating(rating);

            ProductId = productId;
            UserNmae = userName;
            Rating = rating;
            Comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        public void ChangeRating(int? rating)
        {
            if (rating == null && string.IsNullOrWhiteSpace(Comment))
                throw new DomainException( "REVIEW_EMPTY", "Review must contain rating or comment");

            ValidateRating(rating);
            Rating = rating;
        }

        public void ChangeComment(string? comment)
        {
            if (Rating == null && string.IsNullOrWhiteSpace(comment))
                throw new DomainException("REVIEW_EMPTY", "Review must contain rating or comment");

            Comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();
        }

        private static void ValidateRating(int? rating)
        {
            if (rating is null)
                return;

            if (rating < 1 || rating > 5)
                throw new DomainException( "RATING_INVALID", "Rating must be between 1 and 5");
        }

        internal ProductReviewEntity(
            int id,
            int productId,
            string userName,
            int? rating,
            string? comment,
            DateTime createdAt)
        {
            Id = id;
            ProductId = productId;
            UserNmae = userName;
            Rating = rating;
            Comment = comment;
            CreatedAt = createdAt;
        }
    }
}
