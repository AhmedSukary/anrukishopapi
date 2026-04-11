namespace AnrukiShop_API.Requests
{
    public class AddToCartRequest
    {
        public required int CartId { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required decimal Price { get; set; }
    }

    public class UpdateCartItemQuantityRequest
    {
        public required int CartId { get; set; }
        public required int ItemId { get; set; }
        public required int Quantity { get; set; }
    }
}
