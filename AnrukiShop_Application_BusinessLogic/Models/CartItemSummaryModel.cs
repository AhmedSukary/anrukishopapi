namespace AnrukiShop_Application.Models
{
    public class CartItemSummaryModel
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string? PrimaryImageUrl { get; set; }

        public CartItemSummaryModel(int id, int cartId, int productId, int quantity, decimal price, string productName, string? primaryImageUrl)
        {
            Id = id;
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
            ProductName = productName;
            PrimaryImageUrl = primaryImageUrl;
        }
    }
}
