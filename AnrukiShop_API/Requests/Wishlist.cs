namespace AnrukiShop_API.Requests
{
    public class AddToWishlistRequest
    {
        public required int WishlistId { get; set; }
        public required int ProductId { get; set; }
    }
    public class CreateWishlistRequest
    {
        public required int UserId { get; set; }
    }
}
