namespace AnrukiShop_Application.Models
{
    public class WishlistItemModel
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        public int ProductId { get; set; }
    }
}