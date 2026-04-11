namespace AnrukiShop_API.Requests
{
    public class CreateProductReviewRequest
    {
        public required int ProductId { get; set; }
        public required string UserName { get; set; }
        public required int Rating { get; set; }
        public required string Comment { get; set; }
    }

    public class UpdateProductReviewRequest
    {
        public required int Rating { get; set; }
        public required string Comment { get; set; }
    }

    public class CreateProductImageRequest
    {
        public required int ProductId { get; set; }
        public required string Url { get; set; }
        public required bool IsPrimary { get; set; }
    }

    public class UpdateProductImageRequest
    {
        public required string Url { get; set; }
        public required bool IsPrimary { get; set; }
    }

    public class SearchProductsRequest
    {
        public required string Query { get; set; }
    }
    public class CreateProductRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string SKU { get; set; }
        public required int CategoryId { get; set; }
    }

    public class UpdateProductRequest
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int CategoryId { get; set; }
    }
}
