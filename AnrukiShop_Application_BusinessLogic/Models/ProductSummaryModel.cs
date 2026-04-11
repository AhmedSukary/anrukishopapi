namespace AnrukiShop_Application.Models
{
    public class ProductSummaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? PrimaryImageUrl { get; set; }     
        public int? AvgRating { get; set; }
        public int? CommentsCount { get; set; }
        public ProductSummaryModel(int id, string name, string description, decimal price, string? primaryImageUrl, int? avgRating, int? commentsCount)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PrimaryImageUrl = primaryImageUrl;
            AvgRating = avgRating;
            CommentsCount = commentsCount;
        }
    }
}
